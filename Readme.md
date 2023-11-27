# .NET Web API Test task

This is a test task for reading flood-monitoring api.
.Net version - 7

## Setting Up

To setup this project, you need to clone the git repo

```sh
$ git clone https://github.com/m-kuts/Rainfall
$ cd Rainfall/RainfallApi/
```

followed by

```sh
$ dotnet restore
```

and

```sh
$ dotnet run
```

## Run tests

```sh
$ dotnet test
```

## Rainfall API description

```http
GET /Rainfall/id/{stationId}/readings?count=10
```

| Parameter   | Type     | Description                                    |
| :---------- | :------- | :--------------------------------------------- |
| `stationId` | `string` | **Required**. Station ID                       |
| `count`     | `int`    | **Optional**. Count of measures (default - 10) |

## Response

```javascript
{
  "readings": [{
            "dateMeasured": string,
            "amountMeasured": number
        }]
}
```

## Swagger Doc

{API_HOST}/swagger/index.html
