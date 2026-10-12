namespace TællerApp;

public partial class Form1 : Form
{
    private int count;
    private Label countLabel = null!;
    private ListBox logList = null!;

    public Form1()
    {
        InitializeComponent();
        BuildCounterInterface();
    }

    private void BuildCounterInterface()
    {
        Text = "Tæller";
        ClientSize = new Size(480, 560);
        MinimumSize = new Size(420, 500);
        StartPosition = FormStartPosition.CenterScreen;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24),
            ColumnCount = 1,
            RowCount = 3,
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 16));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 44));

        countLabel = new Label
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 42, FontStyle.Bold),
            Text = "0",
            TextAlign = ContentAlignment.MiddleCenter,
        };

        var buttons = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 1,
        };
        for (var column = 0; column < 3; column++)
        {
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 3));
        }

        var incrementButton = CreateButton("+1");
        incrementButton.Click += IncrementButton_Click;
        buttons.Controls.Add(incrementButton, 0, 0);

        var decrementButton = CreateButton("-1");
        decrementButton.Click += DecrementButton_Click;
        buttons.Controls.Add(decrementButton, 1, 0);

        var resetButton = CreateButton("Nulstil");
        resetButton.Click += ResetButton_Click;
        buttons.Controls.Add(resetButton, 2, 0);

        logList = new ListBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10),
            IntegralHeight = false,
        };
        logList.Items.Add("Tæller startet på 0");

        layout.Controls.Add(countLabel, 0, 0);
        layout.Controls.Add(buttons, 0, 1);
        layout.Controls.Add(logList, 0, 2);
        Controls.Add(layout);
    }

    private static Button CreateButton(string text)
    {
        return new Button
        {
            Text = text,
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            Font = new Font("Segoe UI", 11, FontStyle.Regular),
            UseVisualStyleBackColor = true,
        };
    }

    private void IncrementButton_Click(object? sender, EventArgs e)
    {
        UpdateCount(count + 1, "+1");
    }

    private void DecrementButton_Click(object? sender, EventArgs e)
    {
        UpdateCount(count - 1, "-1");
    }

    private void ResetButton_Click(object? sender, EventArgs e)
    {
        UpdateCount(0, "Nulstil");
    }

    private void UpdateCount(int newCount, string action)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        if (newCount is < -10 or > 10)
        {
            MessageBox.Show(
                "Tælleren skal være mellem -10 og 10.",
                "Grænse nået",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            logList.Items.Insert(0, $"{timestamp} - Afvist {action}: tælleren forbliver {count}");
            return;
        }

        count = newCount;
        countLabel.Text = count.ToString();
        logList.Items.Insert(0, $"{timestamp} - {action}: {count}");
    }
}
