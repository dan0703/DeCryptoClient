   M S B A M L   `   `   `  ����F  BDeCryptoWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DeCryptoWPFDeCryptoWPF  5   	   ?$clr-namespace:DeCryptoWPF.PropertiesDeCryptoWPF.Properties  5   	       DeCryptoWPF.MenuGame   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DeCryptoWPF     5   	   2p$clr-namespace:DeCryptoWPF.Properties     5   	     � WindowState$  	Maximized=�5	   	    � Title$ MenuGame��5
   	   $	��450��6   $	��800��6'   .��5      (�  (� Stretch$
 Fill=�6    (� StretchDirection$
 Both=�6   .��5   
   � $
��1920��6   $
��1080��6   
���   �.+�5      �� -   5         ��Name$ Label_MenuGame_DeCrypto��5      $��DeCrypto��65   $��Center=�6H   $��	0,330,0,0q�6e   $	��Top=�6x   
�������$	��150(�6�   $
��Bold"�6�   ��6�   Z� 6�   �� 5      -   5         ��Name$ Button_MenuGame_NewGame��5      &    DeCryptoWPF.Properties.Resources   Button_MenuGame_NewGame#��Z 66   $
��Left=�6o   $��374,671,0,0q�6�   $	��Top=�6�   $��76��6�   $	��430��6�   
����������	#FF2966FB$��24(�6�   $
��Bold"�6  ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5      	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5   "   ��  �� Property �@�Cursor �� Value$
 Hand��6;   5      5      5      �� 5      -   5        $ Button_MenuGame_FindGame��5         Button_MenuGame_FindGame#��Z 67   $
��Left=�6q   $��1080,671,0,0q�6�   $	��Top=�6�   $��76��6�   $	��430��6�   
����������	#FF2966FB$��24(�6�   $
��Bold"�6
  ��5      �� 5      " ��6    5      P� 5      	 �@J�IsMouseOver
 . . �5   "   ��  �@�Cursor$
 Hand��6;   5       5"      5$      �� 5%      -   5         ��Name$  ComboBox_MenuGame_Language��5%      $
��Left=�6;   $��1640,73,0,0q�6V   $	��Top=�6k   $	��215��6�   $��48��6�   $��20(�6�   .@�5&      �� &   ComboBoxItem_MainWindow_Spanish#��Z 6   �� 5'      &   ComboBoxItem_MainWindow_English#��Z 6   5(      �� 5*      -   5         ��Name$$ StackPanel_MenuGame_FriendList��5*      $��20,160,1407,30q�6A   ���#4A5061$y�Hidden=�6n   .+�5+      �  � ColumnDefinitions 5,      �� 5-      $
��Auto �6+   .+�50      �� -   5         ��Name$$ Image_MenuGame_CloseFriendList��50      $ ��/Resources/buttonClose.png��6D   $��40��6h   $��40��6t   $��
450,0,0,15q�6   6�   �� 51      -   5        $ Label_MenuGame_FriendList��51      $��Lista de Amigos��6?   $��40(�6Y   
�������$
��Bold"�6z   $��Center=�6�   $��
79,-1,81,1q�6�   52      �� 53      $	��740��6   
������ ��6C   Z� .@�54      �� $��Friend List��6   
���   �$��20(�6]   $
��Bold"�6k   ��63   Z� .��55      � 
������ $��0,0,0,0q�68   .+�56      �� -   5         ��Name$! ListBox_MenuGame_FriendList��56   &   
������ $��0,0,0,0q�58      $
��Auto=�59      
���   �6N   5:      5;      �� 5<      $��Friend Request��6   
���   �$��20(�6`   $
��Bold"�6n   ��6J   Z� .��5=      � 
������ $��0,0,0,0q�68   .+�5>      �� -	   5        $% ListBox_MenuGame_FriendRequests��5>   &   
������ $��0,0,0,0q�5@      $
��Auto=�5A      
���   �6N   5B      5C      5D      �� 5E      $��4��6   
�������$	��450��66   
�������$��0,5,0,0q�6O   �� 5F      -
   5        $��Button_MenuGame_AddFriend��5F   1   $��Agregar Amigo��6   $��160,20,160,0q�6R   ���#33CC00
�������$��Arial Rounded MT Bold)�6�   $��20(�6�   $
��Bold"�6�   $��35��6�   ��6�   Z� 6�   5H      9� 5J      -   5         9�Name$&  Expander_MenuGame_Configurations��5J   2   $
��Left=�6   $	��124��6\   $
��Menu��6i   $��	39,50,0,0q�6x   $	��Top=�6�   $	��221��6�    9� ExpandDirection$
 Down=�6�   $��LeftToRight=�6�   ���	#FFFBF2F2$
��Bold"�6�   $��24(�6  .��5K      � .+�5L      �� -   5        $* $Button_MenuGame_AccountConfiguration��5L      $��Cuenta��6K    � ToolTip$" Ir a configuracion de Cuenta��6\   $��Center=�6�   $��0,10,0,0q�6�   $	��Top=�6�   $	��218��6�   $��34��6�   $
��Bold"�6�   
�������
���   �$��22(�6N  ��5M      �� 5N      " ��6$    5O   "   P� 5P   &   	 �@J�IsMouseOver
 . . �5Q   *   ��  �@�Cursor$
 Hand��6C   5R   '   5T      5V      �� 5W      -   5        $  Button_MenuGame_FriendList��5W      $��Lista de Amigos��6A   $ Ver lista de Amigos��6[   $��Center=�6y   $��0,44,0,0q�6�   $	��Top=�6�   $	��218��6�   $��38��6�   $
��Bold"�6�   
���   �
�������$��22(�6:  ��5X      �� 5Y      " ��6$    5Z   "   P� 5[   &   	 �@J�IsMouseOver
 . . �5\   *   ��  �@�Cursor$
 Hand��6C   5]   '   5_      5a      5b      5c      5e      5f      5g      