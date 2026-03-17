# Media Asset Export

An Orchard Core module (Class Library) that indexes and exports media asset URLs from published content items using YeSQL indexing. Provides an injectable `IMediaAssetService` and REST API endpoints for querying media assets by content item or content type.

## About this project

> **This is a Class Library (Orchard Core module), not a standalone application.**
>
> Running `dotnet run` will produce the error *"The current OutputType is 'Library'"* — this is **expected**.
> Orchard Core modules cannot run on their own; they must be referenced by an Orchard Core host application.
> Use `dotnet build` to verify that the module compiles correctly.

## Features

- Index media assets from published content items
- Query media assets efficiently using YeSQL LINQ
- REST API endpoints for media asset retrieval
- Support for Azure Storage media references

---

## Integration guide

There are two ways to consume this module in your Orchard Core host application. You can start with the git submodule approach for development and testing, then switch to the NuGet package for production — or use NuGet directly.

### Approach 1 — Git submodule (development / testing)

Use this approach to consume the live source code without publishing to NuGet. This is ideal while you are actively developing the module alongside your host application.

#### 1. Add the submodule to your host application

Inside your Orchard Core host application repository, run:

```bash
# Add the submodule into a subfolder (e.g. modules/)
git submodule add https://github.com/eberteogam/MediaAssetExport.git modules/MediaAssetExport

# Initialise and pull the module source
git submodule update --init --recursive
```

This creates a `modules/MediaAssetExport/` folder containing the full module source and a `.gitmodules` file that records the reference.

#### 2. Reference the module project

Add a `<ProjectReference>` to your host application's `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="modules\MediaAssetExport\MediaAssetExport.csproj" />
</ItemGroup>
```

#### 3. Build and run your host application

```bash
dotnet build
dotnet run
```

Orchard Core will discover the module automatically. Enable the **Media Asset Export** feature in the admin panel (**Admin → Configuration → Features**).

#### Updating the submodule

To pull the latest changes from this repository into your host application:

```bash
git submodule update --remote modules/MediaAssetExport
git add modules/MediaAssetExport
git commit -m "Update MediaAssetExport submodule"
```

---

### Approach 2 — NuGet package (production)

Use this approach once the module has been published to NuGet.org (or when you are ready to stop tracking the source directly).

#### Install the package

```bash
dotnet add package TeoGamarra.MediaAssetExport
```

Or add the `<PackageReference>` manually to your host `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="TeoGamarra.MediaAssetExport" Version="1.0.0" />
</ItemGroup>
```

---

### Migrating from submodule to NuGet

When you are ready to move from tracking live source (submodule) to a versioned NuGet package:

```bash
# 1. Remove the submodule
git submodule deinit -f modules/MediaAssetExport
git rm -f modules/MediaAssetExport
rm -rf .git/modules/modules/MediaAssetExport

# 2. Commit the removal
git commit -m "Remove MediaAssetExport submodule"

# 3. Add the NuGet package reference
dotnet add package TeoGamarra.MediaAssetExport
```

The `<ProjectReference>` in your `.csproj` must be replaced with the `<PackageReference>` above. The API surface (`IMediaAssetService`, endpoints) is identical in both approaches.

---

## Development

### Build the module

```bash
dotnet build
```

### Create a local NuGet package for testing

```bash
dotnet pack --configuration Release
# Package is written to: bin/Release/TeoGamarra.MediaAssetExport.x.y.z.nupkg
```

### CI and releases

| Event | Workflow | Result |
|-------|----------|--------|
| Every push / pull request | `ci.yml` | Builds + packs; uploads `.nupkg` as a workflow artifact |
| Tag `v*.*.*` pushed | `release.yml` | Publishes to NuGet.org via `NUGET_API_KEY` secret |

> **First run:** GitHub requires the repository owner to approve the first workflow run for a new branch or contributor.
> Go to **Actions → CI → (the queued run) → Review pending deployments** and click **Approve and run**.

---

## Usage

1. Reference or install the package in your Orchard Core host project (see above).
2. Enable the **Media Asset Export** feature in the admin panel (**Admin → Configuration → Features**).

### Query media assets

Inject `IMediaAssetService` into any service or controller:

```csharp
// All media URLs from published content items
var allUrls = await _mediaAssetService.GetPublishedMediaAssetsAsync();

// Media URLs for a specific content item
var itemUrls = await _mediaAssetService.GetMediaAssetsByContentItemAsync(contentItemId);

// Media URLs for a specific content type
var typeUrls = await _mediaAssetService.GetMediaAssetsByContentTypeAsync("BlogPost");
```

### REST API endpoints

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

