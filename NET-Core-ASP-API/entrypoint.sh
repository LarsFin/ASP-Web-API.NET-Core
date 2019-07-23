#!/bin/bash

set -e
run_cmd="dotnet /app/bin/Debug/netcoreapp2.2/NET-Core-ASP-API.dll"

until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
shopt -s extglob
rm -rf !(bin)
exec $run_cmd