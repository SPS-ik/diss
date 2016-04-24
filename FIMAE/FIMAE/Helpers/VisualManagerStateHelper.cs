using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FIMAE.Helpers
{
    public class VisualManagerStateHelper : DependencyObject
    {        
        public static readonly DependencyProperty VisualStatePropertyProperty =
            DependencyProperty.RegisterAttached("VisualStateProperty", typeof(string),
                                        typeof(VisualManagerStateHelper),
                                        new UIPropertyMetadata((s, e) =>
                                        {
                                            var propertyName = (string)e.NewValue ??
                                                               (string)e.OldValue;
                                            var ctrl = s as FrameworkElement;
                                            if (ctrl == null)
                                            {
                                                throw new InvalidOperationException(
                                                    "This attached property only supports types derived from Control.");
                                            }
                                            try
                                            {
                                                VisualStateManager.GoToElementState(ctrl, propertyName, true);
                                            }
                                            catch (Exception ex)
                                            {
                                                throw;
                                            }
                                        }));

        public static string GetVisualStateProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(VisualStatePropertyProperty);
        }

        public static void SetVisualStateProperty(DependencyObject obj, string value)
        {
            obj.SetValue(VisualStatePropertyProperty, value);
        }

        #region Methods
        public static DependencyObject GetVisualParentByType(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                    break;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }
        #endregion
    }
}
