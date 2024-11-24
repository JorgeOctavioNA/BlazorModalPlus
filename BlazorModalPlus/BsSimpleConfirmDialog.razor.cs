using Microsoft.AspNetCore.Components;
namespace BlazorModalPlus;
public partial class BsSimpleConfirmDialog
{
    [Parameter]
    public EventCallback<DialogButtonResult> ConfirmCallback { get; set; }
   
    private async Task ButtonClick(DialogButtonResult dialogButton)
    {
        Visible = false;
        await ConfirmCallback.InvokeAsync(dialogButton);
    }

    public async Task ShowDialog(string message, string? title, DialogButtons butonsConfiguration)
    {
        Message = message;
        Title = title ?? "Confirm";
        Buttons = GetButtons(butonsConfiguration);

        // Render the dialog
        Visible = true;

        StateHasChanged();
        await Task.CompletedTask;
    }

    protected IEnumerable<ButtonItem> GetButtons(DialogButtons dialogButtons)
    {
        return dialogButtons switch
        {
            DialogButtons.Ok => new List<ButtonItem>
            {
                new ButtonItem("OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null)
            },
            DialogButtons.OkCancel => new List<ButtonItem>
            {
                new ButtonItem("OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null),
                new ButtonItem("Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            DialogButtons.YesNo => new List<ButtonItem>
            {
                new ButtonItem("Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem("No", BtnRenderStyle.Secondary, DialogButtonResult.No, null)
            },
            DialogButtons.YesNoCancel => new List<ButtonItem>
            {
                new ButtonItem("Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem("No", BtnRenderStyle.Primary, DialogButtonResult.No, null),
                new ButtonItem("Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            _ => throw new NotImplementedException()
        };
    }
}
