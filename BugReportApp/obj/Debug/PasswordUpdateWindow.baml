   M S B A M L   `   `   `  ����G  CBugReportApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BugReportAppBugReportApp  5   	   '    !BugReportApp.PasswordUpdateWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BugReportApp     5   	   -   5          � Title$  Update Password��5   	   $	��300��6!   $	��500��6.    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStyle$ 
ToolWindow=�6   
���OO/�$��1,1,1,1q�6T   
�������.��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��40 �6   �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5   
   �� $��Bottom=�6   $��Center=�6+   $��0��6H   $��2��5      $��12(�6(   $��SemiBold"�66   .��6L   75Please Choose a password. Do not reuse old passwords.5      �� 5    
   $��Center=�6    �� TextAlignment$ Center=�6/   $��1��6F   $��2��5!      $��10(�6(   $��SemiBold"�66   GEPassword must contain at least one capital letter, one reguar letter,�� 5"   S   20one number, and at least 8 characters in length.5$      �� 5'   
   $��2��6   $��0��6   $��Right=�5(      $��SemiBold"�5)      $��Center=�6&   $��10px 0pxq�5*      .��6#   Email:6+   �� 5+   
   -   5         ��Name$ txtEmail��5+      $��2��6$   $��1��61   $��5px 5px 40px 5pxq�6A   $��Center=�5,      $��10px 0pxq�64   �� 5/   
   $��3��6   $��0��6   $��Right=�50      $��SemiBold"�51      $��Center=�6&   $��10px 0pxq�52      .��6#   Old Password:62   G� 53   
   -   5         G�Name$ pwdOldPassword��53      $��3��6.   $��1��6;   $��5px 5px 40px 5pxq�6L   $��Center=�54      $��10px 0pxq�64   �� 57   
   $��4��6   $��0��6   $��Right=�58      $��SemiBold"�59      $��Center=�6&   $��10px 0pxq�5:      .��6#   New Password:62   G� 5;   
   -   5        $ pwdNewPassword��5;      $��4��6.   $��1��6;   $��5px 5px 40px 5pxq�6L   $��Center=�5<      $��10px 0pxq�64   �� 5?   
   $��5��6   $��0��6   $��Right=�5@      $��SemiBold"�5A      $��Center=�6&   $��10px 0pxq�5B      .��6#   Retype Password:65   G� 5C   
   -   5        $ pwdRetypePassword��5C      $��5��61   $��1��6>   $��5px 5px 40px 5pxq�6O   $��Center=�5D      $��10px 0pxq�64   �� 5G   
   -   5        	 ��Name$	 	btnSubmit��5G      $��6��6$   $��1��61   $��Submit��6A   $��5px 5px 40px 5pxq�6R   $��160px��5H      $��Center=�6   
����  �
�������6e   �� 5K   
   -   5        $	 	btnCancel��5K      $��6��6$   $��0��61   $��Cancel��6A   $��10px 5pxq�6R   $
��80px��5L      $��Right=�6   6:   5N      5O      