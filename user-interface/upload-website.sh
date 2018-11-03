#!/usr/bin/env bash

BUCKET="${1:-serverless-telephonist-gui}"
PROFILE="${2:-pattern-match}"

aws s3  sync . "s3://${BUCKET}" --exclude "*.sh" --profile="${PROFILE}" --acl="public-read"
