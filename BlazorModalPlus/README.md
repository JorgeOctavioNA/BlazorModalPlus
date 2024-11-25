# BlazorModalPlus

**BlazorModalPlus** is a Blazor library providing Bootstrap 5-based modal dialog components. These components are highly customizable and cater to a variety of confirmation and dialog use cases.

## Features

- **BsConfirmDialog**:
  - Customizable header and footer using `RenderFragment`.
  - Displays a dialog with a title, message, and dynamically defined buttons.
  - Buttons are configured using `ButtonItem`, allowing custom captions, styles and event callbacks.

- **BsSimpleConfirmDialog**:
  - Simplified for quick confirmations.
  - Displays a title, message, and predefined buttons based on `DialogButtons`.
  - Returns the button action via `EventCallback<DialogButtonResult>`.

Both components support light and dark themes using the Parameter `DarkMode`.

> [!NOTE]
> The DarkMode is available since Bootstrap version 5.3. If you are using an older version, the DarkMode will not work.

---

## Prerequisites
| Version | .NET |
| :--- | :---: |
| 1.0.0 | [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) |

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

These components do not require additional configuration, however they are based on Bootstrap version 5, therefore, it is necessary to include the Bootstrap references if it is not included in your project as follows:

```html
<head>
    ...
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>

```


## How to use
### BsConfirmDialog

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
