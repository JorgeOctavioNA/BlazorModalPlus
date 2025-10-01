# BlazorModalPlus

**BlazorModalPlus** is a Blazor library that provides Bootstrap 5-based modal dialog components. These components are highly customizable and fit a variety of confirmation and dialog use cases. Additionally, multi-language support is added with enhanced accessibility features and improved performance.

## ? Features

- **BsConfirmDialog**:
  - Customizable header and footer using `RenderFragment`.
  - Displays a dialog with a title, message, and dynamically defined buttons.
  - Buttons are configured using `ButtonItem`, allowing custom captions, styles, icons, and event callbacks.
  - Support for custom CSS classes and multiple dialog sizes.

- **BsSimpleConfirmDialog**:
  - Simplified for quick confirmations.
  - Displays a title, message, and predefined buttons based on `DialogButtons`.
  - Returns the button action via `EventCallback<DialogButtonResult>`.
  - Service support for Globalization and Localization.

- **Enhanced Accessibility**:
  - Full ARIA attributes support
  - Keyboard navigation (Escape key to close)
  - Screen reader compatible
  - Focus management

- **Advanced UX Features**:
  - Backdrop click handling
  - Custom dialog sizes (Small, Medium, Large, ExtraLarge, FullScreen)
  - Event callbacks for show/hide operations
  - Programmatic modal service

Both components support light and dark themes using the Parameter `DarkMode`.

> [!NOTE]
> The DarkMode is available since Bootstrap version 5.3. If you are using an older version, the DarkMode will not work.

## ?? What's New in v1.4.0.1

### Performance Improvements
- ? Better dependency injection with direct `IStringLocalizer` injection
- ? Removed unnecessary async operations
- ? Optimized component lifecycle management

### Accessibility Enhancements
- ? Full ARIA attributes support (`aria-modal`, `aria-labelledby`, `aria-describedby`)
- ? Keyboard support (Escape key to close dialogs)
- ? Proper focus management
- ? Screen reader compatibility

### New Features
- ? Additional dialog sizes: `ExtraLarge`, `FullScreen`
- ? Custom CSS classes support via `CustomCssClass` parameter
- ? Backdrop click control with `CloseOnBackdropClick`
- ? Escape key control with `CloseOnEscape`
- ? Dialog lifecycle events: `OnShow`, `OnHide`
- ? Programmatic modal service (`IModalDialogService`)
- ? Enhanced `ButtonItem` with `CssClass` and `IsDisabled` properties

### Code Quality
- ? Parameter validation with proper exceptions
- ? `IDisposable` implementation for resource cleanup
- ? Comprehensive XML documentation
- ? Better error handling in callbacks
- ? Null-safe operations throughout

# Changelog

## [1.4.0.1] - 2025-09-30
### Added
- **Enhanced Accessibility**: Full ARIA attributes, keyboard navigation, and screen reader support
- **New Dialog Sizes**: `ExtraLarge` and `FullScreen` options
- **Advanced UX Controls**: `CloseOnEscape`, `CloseOnBackdropClick` parameters
- **Lifecycle Events**: `OnShow` and `OnHide` event callbacks
- **Custom Styling**: `CustomCssClass` parameter for additional CSS classes
- **Programmatic Service**: `IModalDialogService` for showing dialogs from code
- **Enhanced ButtonItem**: Added `CssClass` and `IsDisabled` properties

### Changed
- **Performance**: Improved dependency injection pattern
- **Validation**: Added parameter validation with proper exceptions
- **Documentation**: Comprehensive XML documentation throughout
- **Resource Management**: Implemented `IDisposable` for proper cleanup

### Fixed
- **Error Handling**: Better exception management in button callbacks
- **Null Safety**: Improved null checking throughout the codebase
- **Memory Leaks**: Proper resource disposal implementation

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
| 1.4.0 | [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) |

## ?? Installation

Add **BlazorModalPlus** to your project via NuGet:

```bash
dotnet add package BlazorModalPlus
```

Add the following to `_Imports.razor`
```razor
@using BlazorModalPlus
```

## ?? Getting Started

> [!NOTE]
> BlazorModalPlus supports the following languages: en-US, es-MX, fr-FR, de-DE, it-IT, ja-JP, pt-PT & zh-CN

### Option 1: With Localization Support

Add the following to your `Program.cs` file:

```csharp
// Add localization and modal service
builder.Services.AddBlazorModalPlusServices(
    supportedCultures: new[] { "en-US", "es-MX" }, 
    defaultCulture: "es-MX",
    includeModalService: true  // Optional: for programmatic usage
);

// ... rest of your configuration

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
```

### Option 2: Basic Setup (No Localization)

If you don't need localization:

```csharp
// Add only the modal service
builder.Services.AddBlazorModalPlusBasicServices(includeModalService: true);
```

### Bootstrap 5 Requirement

The components are based on Bootstrap version 5, so include Bootstrap references if not already in your project:

```html
<head>
    ...
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
```

## ?? How to use

### BsConfirmDialog

```razor
@using BlazorModalPlus

<button class="btn btn-primary" @onclick="ShowDialog">Show Advanced Dialog</button>

<BsConfirmDialog @ref="refConfirmDialog" 
                 Size="DialogSize.Large"
                 CloseOnEscape="true"
                 OnShow="OnDialogShow"
                 OnHide="OnDialogHide">
    <HeaderTemplate>
        <h4 class="text-primary fw-bold">
            <i class="fas fa-cog me-2"></i>
            Advanced Dialog
        </h4>
    </HeaderTemplate>
    <FooterTemplate>
        <InputDate @bind-Value="dateSelected" />
    </FooterTemplate>
</BsConfirmDialog>

@code {
    BsConfirmDialog? refConfirmDialog;
    public DateTime dateSelected { get; set; } = DateTime.Now;

    protected async Task ShowDialog()
    {
        if (refConfirmDialog != null)
        {
            var buttons = new List<ButtonItem>
            {
                new ButtonItem("Save", BtnRenderStyle.Success, 
                    click: EventCallback.Factory.Create(this, SaveAction),
                    iconClass: "fas fa-save"),
                new ButtonItem("Delete", BtnRenderStyle.Danger, 
                    click: EventCallback.Factory.Create(this, DeleteAction),
                    iconClass: "fas fa-trash"),
                new ButtonItem("Cancel", BtnRenderStyle.Secondary, 
                    click: EventCallback.Factory.Create(this, CancelAction))
            };

            await refConfirmDialog.ShowDialog("Choose your action:", "Operations", buttons);
        }
    }

    private async Task OnDialogShow()
    {
        Console.WriteLine("Dialog is now visible");
    }

    private async Task OnDialogHide()
    {
        Console.WriteLine("Dialog was hidden");
    }

    private async Task SaveAction() { /* Your save logic */ }
    private async Task DeleteAction() { /* Your delete logic */ }
    private async Task CancelAction() { /* Your cancel logic */ }
}
```

### BsSimpleConfirmDialog

```razor
@using BlazorModalPlus

<button class="btn btn-warning" @onclick="ShowSimpleDialog">Show Simple Dialog</button>

<BsSimpleConfirmDialog @ref="refSimpleDialog" 
                       Size="DialogSize.Medium"
                       ConfirmCallback="ConfirmationCallback">
</BsSimpleConfirmDialog>

@code {
    BsSimpleConfirmDialog? refSimpleDialog;

    protected async Task ShowSimpleDialog()
    {
        if (refSimpleDialog != null)
            await refSimpleDialog.ShowDialog(
                "Do you want to <b>proceed</b> with this action?", 
                "Confirmation Required", 
                DialogButtons.YesNoCancel
            );
    }

    protected async Task ConfirmationCallback(DialogButtonResult result)
    {
        var message = result switch
        {
            DialogButtonResult.Yes => "User clicked Yes!",
            DialogButtonResult.No => "User clicked No!",
            DialogButtonResult.Cancel => "User cancelled!",
            _ => "Unknown result"
        };
        
        Console.WriteLine(message);
    }
}
```

### Programmatic Usage with Service

```razor
@inject IModalDialogService ModalService

<button @onclick="ShowProgrammaticDialog">Show Service Dialog</button>

<!-- Required: Place this component somewhere in your layout -->
<BsSimpleConfirmDialog @ref="serviceDialog"></BsSimpleConfirmDialog>

@code {
    private BsSimpleConfirmDialog? serviceDialog;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && serviceDialog != null)
        {
            ModalService.SetDialogInstance(serviceDialog);
        }
    }

    private async Task ShowProgrammaticDialog()
    {
        var result = await ModalService.ShowConfirmationAsync(
            "Save changes before closing?", 
            "Unsaved Changes", 
            DialogButtons.YesNoCancel
        );

        Console.WriteLine($"User selected: {result}");
    }
}
```

## ?? Advanced Usage

For more advanced examples and usage patterns, see [ADVANCED_USAGE.md](ADVANCED_USAGE.md).

## ?? Customization Options

### Dialog Sizes
- `DialogSize.Small` - Small modal
- `DialogSize.Medium` - Default size  
- `DialogSize.Large` - Large modal
- `DialogSize.ExtraLarge` - Extra large modal
- `DialogSize.FullScreen` - Full screen modal

### Button Styles
- `BtnRenderStyle.Primary` - Primary button (blue)
- `BtnRenderStyle.Secondary` - Secondary button (gray)
- `BtnRenderStyle.Success` - Success button (green)
- `BtnRenderStyle.Danger` - Danger button (red)
- `BtnRenderStyle.Warning` - Warning button (yellow)
- `BtnRenderStyle.Info` - Info button (cyan)
- `BtnRenderStyle.Light` - Light button
- `BtnRenderStyle.Dark` - Dark button

## ?? Migration Guide

### From v1.3.x to v1.4.0

The library maintains full backward compatibility. All existing code will continue to work without changes. New features are opt-in:

1. **New Parameters**: All new parameters have safe defaults
2. **Enhanced ButtonItem**: Existing constructors still work
3. **Service Integration**: Optional and requires explicit setup

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
