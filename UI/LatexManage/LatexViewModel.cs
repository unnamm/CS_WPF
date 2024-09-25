//WpfMath-License-Identifier: MIT

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMath;
using WpfMath.Parsers;

namespace UI.LatexManage
{
    internal partial class LatexViewModel : ObservableObject
    {
        [ObservableProperty] private string _formula = string.Empty;
        [ObservableProperty] private int _scale;

        public LatexViewModel()
        {
            Formula = @"(x^2 + 2)\cdot(x + 2) = 0"; //sample
            Scale = 50;
        }

        /// <summary>
        /// export png
        /// </summary>
        [RelayCommand]
        public void Print()
        {
            if (string.IsNullOrWhiteSpace(Formula))
            {
                return;
            }

            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image|*.png";

            if (dialog.ShowDialog() == false)
            {
                return;
            }

            try
            {
                File.WriteAllBytes(dialog.FileName, WpfTeXFormulaParser.Instance.Parse(Formula).RenderToPng(Scale, 0.0, 0.0, "Arial"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
    }
}
