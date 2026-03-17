# Media Asset Export

A module for Orchard Core that exports media assets from published content items using YeSQL indexing.

## Features

- Index media assets from published content items
- Query media assets efficiently using LINQ
- REST API endpoints for media asset retrieval
- Support for Azure Storage media references

## Installation

```bash
dotnet add package TeoGamarra.MediaAssetExport
```

## Usage

Enable the **Media Asset Export** feature in the Orchard Core admin panel.

### Query Media Assets

```csharp
var mediaAssets = await _mediaAssetService.GetPublishedMediaAssetsAsync();
```

## Configuration

Configure your media asset extraction in the module settings.

## Author

Teo Gamarra

## License

MIT

