// Copyright (c) Files Community
// Licensed under the MIT License.

using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace Files.App.Storage
{
	[DebuggerDisplay("{" + nameof(ToString) + "()}")]
	public class WindowsBulkOperationsEventArgs(_TRANSFER_SOURCE_FLAGS flags = _TRANSFER_SOURCE_FLAGS.TSF_NORMAL, WindowsStorable? sourceItem = null, WindowsFolder? destinationFolder = null, WindowsStorable? newlyCreated = null, string? name = null, string? templateName = null, HRESULT result = default)
		: EventArgs
	{
		public _TRANSFER_SOURCE_FLAGS Flags { get; set; } = flags;

		public WindowsStorable? SourceItem { get; set; } = sourceItem;

		public WindowsFolder? DestinationFolder { get; set; } = destinationFolder;

		public WindowsStorable? NewlyCreated { get; set; } = newlyCreated;

		public string? Name { get; set; } = name;

		public string? TemplateName { get; set; } = templateName;

		public HRESULT Result { get; protected set; } = result;

		public override string ToString()
			=> $"Hr:\"{Result}\"; Src:\"{SourceItem}\"; Dst:\"{DestinationFolder}\"; New:\"{NewlyCreated}\"; Name:\"{Name}\"";
	}

	public delegate void WindowsBulkOperationsProgressChangedEventHandler(object sender, WindowsBulkOperationsProgressChangedEventArgs e);

	public class WindowsBulkOperationsProgressChangedEventArgs : System.ComponentModel.ProgressChangedEventArgs
	{
		public ulong BytesTransferred { get; }
		public ulong TotalBytes { get; }

		public WindowsBulkOperationsProgressChangedEventArgs(ulong bytesTransferred, ulong totalBytes, object? userState)
			: base(totalBytes > 0 ? (int)(bytesTransferred * 100 / totalBytes) : 0, userState)
		{
			BytesTransferred = bytesTransferred;
			TotalBytes = totalBytes;
		}
	}
}
