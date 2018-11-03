using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

using Amazon.Lambda.Core;

using Amazon.XRay.Recorder.Handlers.System.Net;

using Telephonist.Utilities;

namespace Telephonist
{
  public class PagerDutyClient {
    private string subdomain;
    private string authorization;

    private HttpClient client;

    public PagerDutyClient(string subdomain, string apiToken)
    {
      this.subdomain = subdomain;
      this.authorization = $"Token token={apiToken}";

      this.client = new HttpClient(new HttpClientXRayTracingHandler(new HttpClientHandler()));
      this.client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.authorization);
    }

    public async Task<string> GetCurrentOnCallOperator(string scheduleId)
    {
      DateTime now = DateTime.Now;
      DateTime oneSecondLater = now.AddSeconds(1);

      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["since"] = now.ToString("s", CultureInfo.InvariantCulture);
      parameters["until"] = oneSecondLater.ToString("s", CultureInfo.InvariantCulture);
      parameters["time_zone"] = "UTC";

      string queryString = WebHelpers.ToQueryString(parameters);
      string path = $"https://{this.subdomain}.pagerduty.com/api/v1/schedules/{scheduleId}?{queryString}";

      HttpResponseMessage response = await this.client.GetAsync(path);

      LambdaLogger.Log($"path: {path}");
      LambdaLogger.Log($"Status Code: {response.StatusCode}");

      LogAPICall(PagerDutyAPICallType.GetSchedule).Forget();

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }

      return null;
    }

    public async Task<string> GetUserContactMethods(string userId)
    {
      string path = $"https://{this.subdomain}.pagerduty.com/api/v1/users/{userId}/contact_methods";

      HttpResponseMessage response = await this.client.GetAsync(path);

      LambdaLogger.Log($"path: {path}");
      LambdaLogger.Log($"Status Code: {response.StatusCode}");

      LogAPICall(PagerDutyAPICallType.GetUserContactMethods).Forget();

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }

      return null;
    }

    private sealed class PagerDutyAPICallType {
      private readonly String name;

      public static readonly PagerDutyAPICallType GetSchedule = new PagerDutyAPICallType("GET /api/v1/schedules/{scheduleId}");
      public static readonly PagerDutyAPICallType GetUserContactMethods = new PagerDutyAPICallType("GET /api/v1/users/{userId}/contact_methods");

      private PagerDutyAPICallType(String name)
      {
        this.name = name;
      }

      public override String ToString()
      {
        return name;
      }
    }

    private async Task<PutMetricDataResponse> LogAPICall(PagerDutyAPICallType apiCallType)
    {
      IAmazonCloudWatch client = new AmazonCloudWatchClient();

      Dimension dimension = new Dimension()
      {
        Name = "PagerDuty",
        Value = "API Calls"
      };

      MetricDatum point = new MetricDatum()
      {
        Dimensions = new List<Dimension>() { dimension },
        MetricName = apiCallType.ToString(),
        Unit = StandardUnit.Count,
        StorageResolution = 1,
        Value = 1.0
      };

      PutMetricDataRequest request = new PutMetricDataRequest
      {
        MetricData = new List<MetricDatum>() { point },
        Namespace = "External API Calls"
      };

      return await client.PutMetricDataAsync(request);
    }
  }
}
