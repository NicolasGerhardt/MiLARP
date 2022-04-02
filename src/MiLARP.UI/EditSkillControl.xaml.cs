using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using MiLARP.Application.DataAccess;
using MiLARP.Domain.Models;
using Microsoft.VisualBasic;

namespace MiLARP.UI;

public partial class EditSkillControl : UserControl
{
    public Skill? Skill { get; set; }
    private readonly MainWindow _mainWindow;

    public EditSkillControl(MainWindow mainWindow, Skill? skill = null)
    {
        _mainWindow = mainWindow;
        InitializeComponent();

        Skill = skill ?? new Skill {Id = -1, Name = "Unnamed Skill"};
        UpdateControlView();
    }

    private void UpdateControlView()
    {
        if (Skill is null) return;

        lblId.Content = Skill.Id;
        tbName.Text = Skill.Name;
        tbDescription.Text = Skill.Description;
        tbNotes.Text = Skill.Notes;
        SkillTagStackPanel.Children.Clear();
        if (Skill?.Tags is not null)
        {
            foreach (var tag in Skill.Tags)
            {
                SkillTagStackPanel.Children.Add(new SkillTagControl(this, tag));
            }
        }

        SkillTagStackPanel.Children.Add(AddTagButton());
    }

    private Button AddTagButton()
    {
        var button = new Button();
        button.Content = "➕";
        button.Click += btnAddTagToSkill_OnClick;
        return button;
    }

    private void btnAddTagToSkill_OnClick(object sender, RoutedEventArgs e)
    {
        var result = Interaction.InputBox("Tag Name", "Add Tag").Trim().Replace(' ', '-');

        if (!string.IsNullOrWhiteSpace(result))
        {
            if (Skill.Tags is null) Skill.Tags = new List<Tag>();
            Skill.Tags.Add(new Tag {Text = result});
        }

        UpdateControlView();
    }

    private void btnSave_OnClick(object sender, RoutedEventArgs e)
    {
        Skill.Name = tbName.Text;
        Skill.Description = tbDescription.Text;
        Skill.Notes = tbNotes.Text;
        _mainWindow.SaveSkill(Skill);
    }

    public void RemoveTag(Tag tag)
    {
        Skill.Tags.Remove(tag);
        UpdateControlView();
    }
}