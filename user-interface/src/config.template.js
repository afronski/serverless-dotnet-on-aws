(function (window) {
  "use strict";

  window.ServerlessTelephonist = {
    AWS: {
      UserPoolId: "<Id of Amazon Cognito user pool>",
      IdentityPoolId: "<Id of Amazon Cognito identity pool>",
      ClientId: "<Id of Amazon Cognito client user here>",
      LoginService: "cognito-idp.<AWS region>.amazonaws.com/<Id of Amazon Cognito user pool>",
      Region: "<AWS region>"
    },

    API: {
      GetOnCallOperatorDetailsURL: "https://<Your C#-based Amazon API Gateway endpoint>.execute-api.<AWS region>.amazonaws.com/production/operators/current/details",
      TestOnCallNumberURL: "https://<Your F#-based Amazon API Gateway endpoint>.execute-api.<AWS region>.amazonaws.com/production/calls/test"
    },

    OnCallNumber: "+48<Phone number that will be paged and called by external systems>"
  };

} (window));
