﻿#pragma checksum "..\..\ReceiveFriendRequest.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CB3490318371E221BA1BF6C34C5B8FFDD7D80DEC978A6024186FBD1D7190F386"
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
    /// ReceiveFriendRequest
    /// </summary>
    public partial class ReceiveFriendRequest : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\ReceiveFriendRequest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_ReceiveFriendRequest_SenderNickname;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\ReceiveFriendRequest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_ReceiveFriendRequest_AcceptRequest;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ReceiveFriendRequest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_ReceiveFriendRequest_Accept;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ReceiveFriendRequest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_ReceiveFriendRequest_Cancel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ReceiveFriendRequest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressBar_ReceiverFriendRequest;
        
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
            System.Uri resourceLocater = new System.Uri("/DeCryptoWPF;component/receivefriendrequest.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReceiveFriendRequest.xaml"
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
            this.Label_ReceiveFriendRequest_SenderNickname = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Label_ReceiveFriendRequest_AcceptRequest = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Button_ReceiveFriendRequest_Accept = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\ReceiveFriendRequest.xaml"
            this.Button_ReceiveFriendRequest_Accept.Click += new System.Windows.RoutedEventHandler(this.Button_ReceiveFriendRequest_Accept_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Button_ReceiveFriendRequest_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\ReceiveFriendRequest.xaml"
            this.Button_ReceiveFriendRequest_Cancel.Click += new System.Windows.RoutedEventHandler(this.Button_ReceiveFriendRequest_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ProgressBar_ReceiverFriendRequest = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

