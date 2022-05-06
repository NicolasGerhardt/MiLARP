using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiLARP.Application.DataAccess;
using MiLARP.Domain.Models;
using MiLARP.Infrastructure.Repositories;

namespace MiLARP.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISkillsRepository _skillsRepo;
        
        public MainWindow()
        {
            InitializeComponent();
            _skillsRepo = new SkillsFileRepository("data");
            UpdateSkillsList(_skillsRepo.GetAllSkills());
        }

        private void UpdateSkillsList(IList<Skill?> skills)
        {
            SkillsList.Items.Clear();
            SkillsDockPanel.Children.Clear();
            foreach (var skill in skills)
            {
                SkillsList.Items.Add(skill);
            }
        }

        private void AddSkill_OnClick(object sender, RoutedEventArgs e)
        {
            SkillsDockPanel.Children.Clear();
            SkillsDockPanel.Children.Add(new EditSkillControl(this));
        }

        private void EditSkill_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkillsList.SelectedItem == null) return;
            SkillsDockPanel.Children.Clear();
            SkillsDockPanel.Children.Add(new EditSkillControl(this, (Skill)SkillsList.SelectedItem));
        }

        private void RemoveSkill_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkillsList.SelectedItem is not Skill)
            {
                AlertBox("No skill selected");
                return;
            }

            var skill = (Skill) SkillsList.SelectedItem;

            var result = MessageBox.Show($"Are you sure you want to remove \"{skill}\"", "REMOVE SKILL VERIFICATION",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                UpdateSkillsList(_skillsRepo.RemoveSkill(skill));
            }
        }
        private void AlertBox(string text = "Not Implemented", MessageBoxImage icon = MessageBoxImage.Warning)
        {
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
        }

        public void SaveSkill(Skill? skill)
        {
            UpdateSkillsList(skill.Id < 0 ? _skillsRepo.AddSkill(skill) : _skillsRepo.UpdateSkill(skill));
        }
    }
}