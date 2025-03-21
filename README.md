# BlazorModalPlus

**BlazorModalPlus** is a Blazor library that provides Bootstrap 5-based modal dialog components. These components are highly customizable and fit a variety of confirmation and dialog use cases. Additionally, multi-language support is added.

## Features

- **BsConfirmDialog**:
  - Customizable header and footer using `RenderFragment`.
  - Displays a dialog with a title, message, and dynamically defined buttons.
  - Buttons are configured using `ButtonItem`, allowing custom captions, styles and event callbacks.

- **BsSimpleConfirmDialog**:
  - Simplified for quick confirmations.
  - Displays a title, message, and predefined buttons based on `DialogButtons`.
  - Returns the button action via `EventCallback<DialogButtonResult>`.
  - Service to support for Globalization and Localization

Both components support light and dark themes using the Parameter `DarkMode`.

> [!NOTE]
> The DarkMode is available since Bootstrap version 5.3. If you are using an older version, the DarkMode will not work.

# Changelog

## [1.3.0] - 2025-03-08
### Added
- **BsConfirmDialog**:
  - Support for different dialog sizes (`DialogSize`).
  - Support for icons in buttons (`ButtonItem.IconClass`).

- **BsSimpleConfirmDialog**:
  - Support for icons in buttons (`ButtonItem.IconClass`).

### Changed
- **BsConfirmDialog**:
  - Null check for `localizer` and use of default values when not available.

- **BsSimpleConfirmDialog**:
  - Null check for `localizer` and use of default values when not available.

### Fixed
- Minor style corrections and reordering of CSS classes to improve readability and maintainability of the code.
---

## Prerequisites
| Version | .NET |
| :--- | :---: |
| 1.3.0 | [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) |

## Installation

Add **BlazorModalPlus** to your project via NuGet:

```bash
dotnet add package BlazorModalPlus
```

Add the following to `_Imports.razor`
```razor
@using BlazorModalPlus
```

## Getting Started

> [!NOTE]
> BlazorModalPlus supports the following languages: en-US, es-MX, fr-FR, de-DE, it-IT, ja-JP, pt-PT & zh-CN

To use language support (optional), add the following to your `Program.cs` file:

```csharp
// In this case, we use "es-MX" as the default culture and support both "en-US" and "es-MX".
builder.Services.AddBlazorModalPlusServices(
    supportedCultures: new[] { "en-US","es-MX" }, 
    defaultCulture: "es-MX");

...

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
```

The components are based on Bootstrap version 5, then, it is necessary to include the Bootstrap references if it is not included in your project as follows:

```html
<head>
    ...
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>

```

## How to use
![Usage](Usage.jpg)

### BsConfirmDialog

![BsConfirmDialog](BsConfirmDialog.jpg)
```razor
@using BlazorModalPlus

<h3>Dialog Modal</h3>

<button class="btn btn-primary" @onclick="BtnClick">
    Show dialog
</button>

<hr />

<p>
    @actionMessage
</p>

<BsConfirmDialog @ref="refConfirmDialog">
    <HeaderTemplate>
        <h4 class="text-primary fw-bold">Dialog Modal</h4>
    </HeaderTemplate>

    <FooterTemplate>
        <InputDate @bind-Value="dateSelected" />
    </FooterTemplate>
</BsConfirmDialog>

@code {
    BsConfirmDialog? refConfirmDialog;
    public DateTime dateSelected { get; set; } = DateTime.Now;
    public string actionMessage { get; set; } = string.Empty;

    protected async Task BtnClick()
    {
        if (refConfirmDialog != null)
        {
            IEnumerable<ButtonItem> buttons = new List<ButtonItem>
            {
                new ButtonItem("Process", BtnRenderStyle.Primary, null, EventCallback.Factory.Create(this, ProcessActionDialog)),
                new ButtonItem("Cancel", BtnRenderStyle.Warning, null, EventCallback.Factory.Create(this, CancelActionDialog))
            };

            await refConfirmDialog.ShowDialog("Process action?", null, buttons);
        }
    }

    #region Dialog Actions
    private async Task ProcessActionDialog()
    {
        await Task.Run(() => actionMessage = $"Process button pressed with date: {dateSelected.ToShortDateString()} selected");
    }
    private async Task CancelActionDialog()
    {
        await Task.Run(() => actionMessage = $"Cancel button pressed with date: {dateSelected.ToShortDateString()} selected");
    }
    #endregion

}
```

### BsSimpleConfirmDialog
![BsSimpleConfirmDialog](BsSimpleConfirmDialog.jpg)
```razor
@using BlazorModalPlus

<h3>Simple Dialog Modal</h3>

<button class="btn btn-primary" @onclick="BtnClick">
    Show dialog
</button>

<hr />

<p>
    @actionMessage
</p>


<BsSimpleConfirmDialog @ref="refConfirmSimple" ConfirmCallback="ConfirmationCallback">
</BsSimpleConfirmDialog>

@code {
    BsSimpleConfirmDialog? refConfirmSimple;
    public string actionMessage { get; set; } = string.Empty;

    protected async Task BtnClick()
    {
        if (refConfirmSimple != null)
            await refConfirmSimple.ShowDialog("You need <b>more</b> information?", "Please confirm", DialogButtons.YesNoCancel);
    }

    protected async Task ConfirmationCallback(DialogButtonResult result)
    {
        actionMessage = result switch
        {
            DialogButtonResult.Yes => "Yes button pressed!",
            DialogButtonResult.No => "No button pressed!",
            DialogButtonResult.Cancel => "Cancel button pressed!",
            _ => string.Empty
        };
        
        await Task.CompletedTask;
    }
}

```
