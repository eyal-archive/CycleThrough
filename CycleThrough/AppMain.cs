namespace CycleThrough
{
	using System;
	using System.Windows.Forms;

	public partial class AppMain : Form
	{
		public AppMain()
		{
			InitializeComponent();

			// Add the KeyDown event
			maskedTextBox1.KeyDown += TextBox1_KeyDown;
			// Add the Enter event
			maskedTextBox1.Enter += TextBox1_Enter;
			// Add the Leave event
			maskedTextBox1.Leave += TextBox1_Leave;
		}

		// Handle the Enter event
		private void TextBox1_Enter(object sender, EventArgs e)
		{
			// Resets the cursor when we enter the textbox
			maskedTextBox1.SelectionStart = 0;

			// Disable the TabStop property to prevent the form and its controls to catch the Tab key
			foreach (Control c in Controls)
			{
				c.TabStop = false;
			}
		}

		// Handle the KeyDown event
		private void TextBox1_KeyDown(object sender, KeyEventArgs e)
		{
			// Cycle through the mask fields
			if (e.KeyCode != Keys.Tab)
			{
				return;
			}

			int pos = maskedTextBox1.SelectionStart;
			int max = maskedTextBox1.MaskedTextProvider.Length - maskedTextBox1.MaskedTextProvider.EditPositionCount;
			int nextField = 0;

			for (int i = 0; i < maskedTextBox1.MaskedTextProvider.Length; i++)
			{
				if (!maskedTextBox1.MaskedTextProvider.IsEditPosition(i) && (pos + max) >= i)
				{
					nextField = i;
				}
			}

			nextField += 1;

			// We're done, enable the TabStop property again
			if (pos == nextField)
			{
				TextBox1_Leave(this, e);
			}

			maskedTextBox1.SelectionStart = nextField;
		}

		// Handle the Leave event
		private void TextBox1_Leave(object sender, EventArgs e)
		{
			// Resets the cursor when we leave the textbox
			maskedTextBox1.SelectionStart = 0;

			// Enable the TabStop property so we can cycle through the form controls again
			foreach (Control c in Controls)
			{
				c.TabStop = true;
			}
		}
	}
}