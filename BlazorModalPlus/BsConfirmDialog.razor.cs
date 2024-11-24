using Microsoft.AspNetCore.Components;
namespace BlazorModalPlus;
public partial class BsConfirmDialog : BsConfirmDialogBase
{
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; } = null;

    [Parameter]
    public RenderFragment? FooterTemplate { get; set; } = null;


    private async Task ButtonClick(EventCallback eventCallback)
    {
        Visible = false;
        await eventCallback.InvokeAsync();
    }

    /// <summary>
    /// Shows the dialog with the specified message, title and buttons.
    /// </summary>
    /// <param name="message">The message for Modal Dialog</param>
    /// <param name="title">Text for the title at the top of the modal dialog. Ignored when HeaderTemplate is defined </param>
    /// <param name="buttons">Array of <see cref="ButtonItem" /></param>
    public async Task ShowDialog(string message, string? title, IEnumerable<ButtonItem>? buttons)
    {
        Message = message;
        Title = title ?? "Confirm";
        Buttons = buttons;

        // Render the dialog
        Visible = true;

        StateHasChanged();
        await Task.CompletedTask;
    }

}