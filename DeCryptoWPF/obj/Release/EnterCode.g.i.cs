﻿#pragma checksum "..\..\EnterCode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BC7FBFBD8D49CD5C91F14F14945C7F9715A1D954BAEA43FEF3220D3ACC1F190A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using DeCryptoWPF;
using DeCryptoWPF.Properties;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DeCryptoWPF {
    
    
    /// <summary>
    /// EnterCode
    /// </summary>
    public partial class EnterCode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_CodeWindow_EnterCode;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_CodeWindow_Join;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_CodeWindow;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox_Code;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DeCryptoWPF;component/entercode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EnterCode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Label_CodeWindow_EnterCode = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Button_CodeWindow_Join = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\EnterCode.xaml"
            this.Button_CodeWindow_Join.Click += new System.Windows.RoutedEventHandler(this.Button_RegisterUser_Enter_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Button_CodeWindow = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\EnterCode.xaml"
            this.Button_CodeWindow.Click += new System.Windows.RoutedEventHandler(this.Button_RegisterUser_Enter_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBox_Code = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\EnterCode.xaml"
            this.TextBox_Code.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_Code_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

