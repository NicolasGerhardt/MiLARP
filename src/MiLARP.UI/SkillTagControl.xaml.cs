using System.Windows;
using System.Windows.Controls;
using MiLARP.Domain.Models;

namespace MiLARP.UI;

public partial class SkillTagControl : UserControl
{
    private EditSkillControl _parent;
    private Tag _tag;
    public SkillTagControl(EditSkillControl parent, Tag tag)
    {
        InitializeComponent();
        lblTagText.Content = tag.Text;
        _parent = parent;
        _tag = tag;
    }

    private void Remove_OnClick(object sender, RoutedEventArgs e)
    {
        _parent.RemoveTag(_tag);
    }
}