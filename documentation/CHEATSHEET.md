# Cheatsheet

## Diagram

<p align="center">
  <img src="https://github.com/pattern-match/serverless-dotnet-on-aws/raw/master/documentation/diagram.png" />
</p>

## Presentation

1. What we will build?
2. Present *Amazon Cognito* identity directory and our user.
3. Present *Amazon S3* and our *Single Page Application*.
4. Present *Amazon API Gateway*.
5. Present both *Amazon Lambda* functions from the outside.
6. Show code that is used to fetch the current on-call operator.
7. Present *PagerDuty* schedule and how it works.
8. Present main number in *Twilio* and what will be executed in response after calling that via phone.
9. Show *C#* code that is used in response to a phone call and then redirect to a proper number.
10. Choose one person from the audience with a phone, ask it to stand, and describe what she will hear after dialing the on-call number.
  - Present a ringing phone and answer the call.
11. Present secondary number in *Twilio*, and explain why we need a button for testing on-call shift.
  - Show *F#* code that is used in to invoke a programmatic phone call.
  - Click the button, wait for a call, and enable speaker in order to hear that *Twilio* automatically called you.
12. Show how logs are gathered in the *Amazon CloudWatch* console.
13. Show how metrics are gathered in the *Amazon CloudWatch* console.
14. Show how you can debug and trace with use of *AWS X-Ray*.

## FAQ

1. **Q**: Why you have separated *C#* and *F#* projects?
  - **A**: There is no way to share different languages inside one project, you can do it only via multiple projects in a single solution.
