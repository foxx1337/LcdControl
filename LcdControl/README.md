# LcdControl

## Requirements
1. get the [Logitech LCD SDK](http://gaming.logitech.com/en-us/developers)
2. ensure the wrapper dll - `LogitechLedEnginesWrapper.dll` - is placed somewhere in the path

## Configuration
File `LcdControl.json` is required for configuration data. It looks like:

	
```json
{
    queryInterval: 5,
    gerritUrl: "https://some.such.org/gerrit",
    gerritUser: "foo_user",
    gerritPassword: "gerrit_http_access_password"
}
```

For the gerrit password see [the docs](https://gerrit-review.googlesource.com/Documentation/rest-api.html#authentication).