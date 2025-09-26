# BlazorModalPlus - Advanced Usage Guide

## New Features in v1.4.0

### 1. Extended Dialog Sizes

```csharp
// Extra large dialog
<BsConfirmDialog Size="DialogSize.ExtraLarge" @ref="refDialog">
    <!-- content -->
</BsConfirmDialog>

// Full screen dialog
<BsSimpleConfirmDialog Size="DialogSize.FullScreen" @ref="refSimpleDialog">
</BsSimpleConfirmDialog>
```

### 2. Behavior Control

```csharp
// Disable closing with Escape key
<BsConfirmDialog CloseOnEscape="false" @ref="refDialog">
    <!-- content -->
</BsConfirmDialog>

// Disable closing by clicking outside the modal
<BsSimpleConfirmDialog CloseOnBackdropClick="false" @ref="refSimpleDialog">
</BsSimpleConfirmDialog>
```

### 3. Lifecycle Events

```csharp
<BsConfirmDialog @ref="refDialog" OnShow="OnDialogShow" OnHide="OnDialogHide">
    <!-- content -->
</BsConfirmDialog>

@code {
    private async Task OnDialogShow()
    {
        Console.WriteLine("Dialog shown");
    }

    private async Task OnDialogHide()
    {
        Console.WriteLine("Dialog hidden");
    }
}
```

### 4. Custom CSS Classes

```csharp
<BsConfirmDialog CustomCssClass="my-custom-dialog-class" @ref="refDialog">
    <!-- content -->
</BsConfirmDialog>
```

### 5. Advanced Button States

```csharp
protected async Task ShowAdvancedDialog()
{
    var buttons = new List<ButtonItem>
    {
        new ButtonItem(
            caption: "Save", 
            renderStyle: BtnRenderStyle.Success, 
            click: EventCallback.Factory.Create(this, SaveAction),
            iconClass: "fas fa-save",
            isDisabled: false
        ),
        new ButtonItem(
            caption: "Delete", 
            renderStyle: BtnRenderStyle.Danger, 
            click: EventCallback.Factory.Create(this, DeleteAction),
            iconClass: "fas fa-trash",
            cssClass: "btn-outline-danger"
        )
    };

    await refDialog.ShowDialog("Confirm operation?", "Action Required", buttons);
}
```

### 6. Programmatic Modal Service

#### Configuration in Program.cs

```csharp
// With localization
builder.Services.AddBlazorModalPlusServices(
    supportedCultures: new[] { "es-MX", "en-US" }, 
    defaultCulture: "es-MX",
    includeModalService: true
);

// Without localization (service only)
builder.Services.AddBlazorModalPlusBasicServices(includeModalService: true);
```

#### Using the Service

```csharp
@inject IModalDialogService ModalService

<button @onclick="ShowProgrammaticDialog">Show Dialog</button>

<!-- This component must be present in the layout or page -->
<BsSimpleConfirmDialog @ref="modalDialog"></BsSimpleConfirmDialog>

@code {
    private BsSimpleConfirmDialog modalDialog;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            ModalService.SetDialogInstance(modalDialog);
        }
    }

    private async Task ShowProgrammaticDialog()
    {
        var result = await ModalService.ShowConfirmationAsync(
            "Do you want to continue with the operation?", 
            "Confirmation", 
            DialogButtons.YesNoCancel
        );

        Console.WriteLine($"User selected: {result}");
    }
}
```

### 7. Accessibility Best Practices

The components now automatically include:

- **ARIA attributes**: `aria-modal`, `aria-labelledby`, `aria-describedby`
- **Semantic roles**: `role="dialog"`
- **Keyboard support**: Escape to close
- **Focus management**: Close button available
- **Screen reader support**: Icons marked as `aria-hidden="true"`

### 8. Improved Error Handling

```csharp
try 
{
    await refDialog.ShowDialog("", "Title", buttons); // Will throw ArgumentException
}
catch (ArgumentException ex)
{
    // Handle parameter validation
    Console.WriteLine($"Error: {ex.Message}");
}
```

### 9. Complete Advanced Usage Example

```razor
@page "/advanced-modal-example"
@using BlazorModalPlus
@inject IModalDialogService ModalService

<h3>BlazorModalPlus Advanced Example</h3>

<div class="row">
    <div class="col-md-4">
        <button class="btn btn-primary" @onclick="ShowCustomDialog">
            <i class="fas fa-cog me-1"></i>
            Custom Dialog
        </button>
    </div>
    <div class="col-md-4">
        <button class="btn btn-success" @onclick="ShowServiceDialog">
            <i class="fas fa-service me-1"></i>
            Use Service
        </button>
    </div>
    <div class="col-md-4">
        <button class="btn btn-warning" @onclick="ShowFullScreenDialog">
            <i class="fas fa-expand me-1"></i>
            Full Screen
        </button>
    </div>
</div>

<div class="mt-3">
    <p><strong>Result:</strong> @actionResult</p>
</div>

<!-- Custom dialog -->
<BsConfirmDialog @ref="customDialog" 
                 Size="DialogSize.Large" 
                 DarkMode="DarkModeDialog.Dark"
                 OnShow="OnCustomDialogShow"
                 OnHide="OnCustomDialogHide"
                 CloseOnBackdropClick="false">
    <HeaderTemplate>
        <h4 class="text-warning">
            <i class="fas fa-exclamation-triangle me-2"></i>
            Important Warning
        </h4>
    </HeaderTemplate>
    <FooterTemplate>
        <small class="text-muted">This action cannot be undone</small>
    </FooterTemplate>
</BsConfirmDialog>

<!-- Dialog for service -->
<BsSimpleConfirmDialog @ref="serviceDialog"></BsSimpleConfirmDialog>

<!-- Full screen dialog -->
<BsConfirmDialog @ref="fullScreenDialog" Size="DialogSize.FullScreen">
    <HeaderTemplate>
        <h2 class="text-primary">Full View</h2>
    </HeaderTemplate>
</BsConfirmDialog>

@code {
    private BsConfirmDialog? customDialog;
    private BsSimpleConfirmDialog? serviceDialog;
    private BsConfirmDialog? fullScreenDialog;
    private string actionResult = "No action performed";

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && serviceDialog != null)
        {
            ModalService.SetDialogInstance(serviceDialog);
        }
    }

    private async Task ShowCustomDialog()
    {
        var buttons = new List<ButtonItem>
        {
            new ButtonItem("Proceed", BtnRenderStyle.Danger, 
                click: EventCallback.Factory.Create(this, ProceedAction),
                iconClass: "fas fa-check"),
            new ButtonItem("Cancel", BtnRenderStyle.Secondary,
                click: EventCallback.Factory.Create(this, CancelAction),
                iconClass: "fas fa-times")
        };

        await customDialog.ShowDialog(
            "This action will delete all data. Are you sure you want to continue?", 
            null, 
            buttons
        );
    }

    private async Task ShowServiceDialog()
    {
        var result = await ModalService.ShowConfirmationAsync(
            "Do you want to save pending changes?",
            "Save Changes",
            DialogButtons.YesNoCancel
        );

        actionResult = $"Modal service - Result: {result}";
        StateHasChanged();
    }

    private async Task ShowFullScreenDialog()
    {
        var buttons = new List<ButtonItem>
        {
            new ButtonItem("Close", BtnRenderStyle.Primary,
                click: EventCallback.Factory.Create(this, CloseFullScreenAction))
        };

        await fullScreenDialog.ShowDialog(
            "This is an example of a full-screen dialog. It can include much more content and complex controls.",
            null,
            buttons
        );
    }

    #region Event Handlers
    private async Task OnCustomDialogShow()
    {
        Console.WriteLine("Custom dialog shown");
    }

    private async Task OnCustomDialogHide()
    {
        Console.WriteLine("Custom dialog hidden");
    }

    private async Task ProceedAction()
    {
        actionResult = "PROCEED action executed";
        StateHasChanged();
    }

    private async Task CancelAction()
    {
        actionResult = "CANCEL action executed";
        StateHasChanged();
    }

    private async Task CloseFullScreenAction()
    {
        actionResult = "Full screen dialog closed";
        StateHasChanged();
    }
    #endregion
}
```

## Migration Notes from v1.3.0

1. **Dependency Injection**: Components now inject `IStringLocalizer<Language>` directly instead of `IServiceProvider`
2. **New Parameters**: Added `CloseOnEscape`, `CloseOnBackdropClick`, `OnShow`, `OnHide`, and `CustomCssClass`
3. **Validation**: `ShowDialog` methods now validate input parameters
4. **IDisposable**: Components implement `IDisposable` for resource cleanup
5. **ButtonItem**: Added `CssClass` and `IsDisabled` properties

## Backward Compatibility

All existing functionality continues to work without changes. New features are optional and have safe default values.