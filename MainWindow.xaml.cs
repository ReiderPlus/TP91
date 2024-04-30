using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts;
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

namespace TP91
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BuildHistogram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Построение диаграммы (столбчатой)
                ColumnChart.Series.Clear();

                ChartValues<int> values = new ChartValues<int>();
                string[] dataValues = HistogramDataTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string value in dataValues)
                {
                    values.Add(Convert.ToInt32(value));
                }

                ColumnSeries series = new ColumnSeries
                {
                    Title = "Диаграмма",
                    Values = values,
                    Fill = new SolidColorBrush(GetSelectedColor(ColumnFillColorComboBox))
                };

                ColumnChart.Series.Add(series);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для данных диаграммы.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void BuildChart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Построение графика
                LineChart.Series.Clear();

                ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>
                {
                    new ObservablePoint(Convert.ToDouble(Point1X.Text), Convert.ToDouble(Point1Y.Text)),
                    new ObservablePoint(Convert.ToDouble(Point2X.Text), Convert.ToDouble(Point2Y.Text)),
                    new ObservablePoint(Convert.ToDouble(Point3X.Text), Convert.ToDouble(Point3Y.Text))
                };

                LineSeries series = new LineSeries
                {
                    Title = "График",
                    Values = values,
                    Stroke = new SolidColorBrush(GetSelectedColor(LineStrokeColorComboBox)),
                    Fill = new SolidColorBrush(GetSelectedColor(LineFillColorComboBox)),
                    StrokeThickness = 2
                };

                LineChart.Series.Add(series);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для координат точек.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        // Метод, который будет вызываться при нажатии кнопки "Применить форматирование"
        private void ApplyFormatting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Применение форматирования к графику и диаграмме

                // Получаем выбранные цвета из ComboBox и создаем кисти
                SolidColorBrush fillBrush = new SolidColorBrush(GetSelectedColor(ColumnFillColorComboBox));
                SolidColorBrush borderBrush = new SolidColorBrush(GetSelectedColor(ColumnBorderColorComboBox));

                // Применяем форматирование к диаграмме (столбчатой)
                if (ColumnChart.Series.Count > 0 && ColumnChart.Series[0] is ColumnSeries)
                {
                    ColumnSeries series = (ColumnSeries)ColumnChart.Series[0];
                    series.Fill = fillBrush;
                    series.Stroke = borderBrush;
                }

                // Применяем форматирование к графику (линейному)
                if (LineChart.Series.Count > 0 && LineChart.Series[0] is LineSeries)
                {
                    LineSeries series = (LineSeries)LineChart.Series[0];
                    series.Fill = fillBrush;
                    series.Stroke = borderBrush;
                }

                MessageBox.Show("Форматирование успешно применено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при применении форматирования: {ex.Message}");
            }
        }

        // Метод для получения выбранного цвета из ComboBox
        private Color GetSelectedColor(ComboBox comboBox)
        {
            string colorName = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            switch (colorName)
            {
                case "Белый":
                    return Colors.White;
                case "Чёрный":
                    return Colors.Black;
                case "Красный":
                    return Colors.Red;
                case "Голубой":
                    return Colors.Blue;
                case "Фиолетовый":
                    return Colors.Purple;
                default:
                    return Colors.Black; // По умолчанию возвращаем чёрный цвет
            }
        }

        private void ApplyHistogramFormatting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Форматирование диаграммы (столбчатой)
                SolidColorBrush fillBrush = new SolidColorBrush(GetSelectedColor(ColumnFillColorComboBox));
                SolidColorBrush borderBrush = new SolidColorBrush(GetSelectedColor(ColumnBorderColorComboBox));

                if (ColumnChart.Series.Count > 0 && ColumnChart.Series[0] is ColumnSeries)
                {
                    ColumnSeries series = (ColumnSeries)ColumnChart.Series[0];
                    series.Fill = fillBrush;
                    series.Stroke = borderBrush;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при применении форматирования диаграммы: {ex.Message}");
            }
        }

        private void ApplyChartFormatting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Применение форматирования к графику
                SolidColorBrush strokeBrush = new SolidColorBrush(GetSelectedColor(LineStrokeColorComboBox));
                SolidColorBrush fillBrush = new SolidColorBrush(GetSelectedColor(LineFillColorComboBox));

                if (LineChart.Series.Count > 0 && LineChart.Series[0] is LineSeries)
                {
                    LineSeries series = (LineSeries)LineChart.Series[0];
                    series.Stroke = strokeBrush;
                    series.Fill = fillBrush;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при применении форматирования графика: {ex.Message}");
            }
        }
    }
}
