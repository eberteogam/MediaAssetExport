# Media Asset Export

A module for Orchard Core that exports media assets from published content items using YeSQL indexing.

## About this project

> **This is a Class Library (Orchard Core module), not a standalone application.**
>
> Running `dotnet run` will produce the error *"The current OutputType is 'Library'"* — this is expected.
> Orchard Core modules cannot run on their own; they must be referenced by an Orchard Core host application.
> Use `dotnet build` to verify that the module compiles correctly.

## Features

- Index media assets from published content items
- Query media assets efficiently using LINQ
- REST API endpoints for media asset retrieval
- Support for Azure Storage media references

## Installation

### Option A — NuGet package (once published)

```bash
dotnet add package TeoGamarra.MediaAssetExport
```

### Option B — Project reference (local development)

Add a `<ProjectReference>` to your Orchard Core host `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\MediaAssetExport\MediaAssetExport.csproj" />
</ItemGroup>
```

## Development

To verify the module compiles:

```bash
dotnet build
```

To publish a local NuGet package for testing:

```bash
dotnet pack --configuration Release
```

## Usage

1. Reference (or install) the package in your Orchard Core host project.
2. Enable the **Media Asset Export** feature in the Orchard Core admin panel (**Admin → Configuration → Features**).

### Query Media Assets

Inject `IMediaAssetService` into any service or controller:

```csharp
// All media URLs from published content items
var allUrls = await _mediaAssetService.GetPublishedMediaAssetsAsync();

// Media URLs for a specific content item
var itemUrls = await _mediaAssetService.GetMediaAssetsByContentItemAsync(contentItemId);

// Media URLs for a specific content type
var typeUrls = await _mediaAssetService.GetMediaAssetsByContentTypeAsync("BlogPost");
```

### REST API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/media-assets/published` | All media URLs from published content |
| GET | `/api/media-assets/content-item/{contentItemId}` | Media URLs for a specific content item |
| GET | `/api/media-assets/content-type/{contentType}` | Media URLs for a specific content type |

## Configuration

No additional configuration is required. The module registers its YeSQL index automatically when the feature is enabled.

## Author

Teo Gamarra

## License

MIT

