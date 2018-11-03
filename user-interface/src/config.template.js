(function (window) {
  "use strict";

  window.ServerlessTelephonist = {
    AWS: {
      UserPoolId: "<USER_POOL_ID>",
      IdentityPoolId: "<IDENTITY_POOL_ID>",
      ClientId: "<CLIENT_ID>",
      LoginService: "cognito-idp.<AWS_REGION>.amazonaws.com/<USER_POOL_ID>",
      Region: "<AWS_REGION>"
    },

    API: {
      GetOnCallOperatorDetailsURL: "https://<YOUR_CSHARP_API_NAME>.execute-api.<AWS_REGION>.amazonaws.com/production/operators/current/details",
      TestOnCallNumberURL: "https://<YOUR_FSHARP_API_NAME>.execute-api.<AWS_REGION>.amazonaws.com/production/calls/test"
    },

    OnCallNumber: "<ON_CALL_NUMBER>"
  };

} (window));
