using Microsoft.AspNetCore.Components;

namespace BlazorModalPlus
{
    /// <summary>
    /// Defines the contract for modal dialog components
    /// </summary>
    public interface IModalDialog : IDisposable
    {
        /// <summary>
        /// Shows the modal dialog
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the dialog</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ShowDialog(string message, string? title = null);

        /// <summary>
        /// Hides the modal dialog
        /// </summary>
        /// <returns>A task representing the asynchronous operation</returns>
        Task HideDialog();

        /// <summary>
        /// Gets or sets whether the dialog is currently visible
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Event fired when the dialog is shown
        /// </summary>
        EventCallback OnShow { get; set; }

        /// <summary>
        /// Event fired when the dialog is hidden
        /// </summary>
        EventCallback OnHide { get; set; }
    }

    /// <summary>
    /// Defines the contract for modal dialog services
    /// </summary>
    public interface IModalDialogService
    {
        /// <summary>
        /// Shows a confirmation dialog with the specified parameters
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the dialog</param>
        /// <param name="buttons">The button configuration</param>
        /// <returns>The dialog result</returns>
        Task<DialogButtonResult> ShowConfirmationAsync(string message, string? title = null, DialogButtons buttons = DialogButtons.OkCancel);

        /// <summary>
        /// Shows an information dialog with the specified message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the dialog</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ShowInfoAsync(string message, string? title = null);
    }
}