   M S B A M L   `   `   `  ����B  >FStored, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:FStoredFStored  5   	   #    FStored.OrderDetailManagement   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:FStored     5   	     � Title$  OrderDetailManagement��5   	   $	��450��6'   $	��800��64   .��5	      � .+�5
   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��	0,12,0,12q�62   .+�5      �� -   5         ��Name$ 
txtOrderId��5      $��39��6.   ${�False��69   $y�Hidden=�6�   ��6K   ��  �� Path$ OrderId� �� Mode$ OneWay=� � DataContext 6v   �� *SelectedItem+ �� ElementName$ lsvOrderDetails��6�   �� 5      -   5        $ txtProductId��5      $��39��60   ${�False��6;   $y�Hidden=�6�   ��6M   �� $ 	ProductId�$ OneWay=� 6z   �� *SelectedItem+$ lsvOrderDetails��6�   5      �� 5      -   5         ��Name$ lsvOrderDetails��5      $	��310��60    �� View 5      �� 5      .P�5      �� $��Product Name��6)   	 �� Width$		 125��6?   
 �� DisplayMemberBinding
 6K   �� *Product.ProductName+�� 5      $��
Unit Price��6)   $		 230��6=   
 6I   �� *	UnitPrice+�� 5      $��Quantity��6)   $		 120��6;   
 6G   �� *
Quantity+�� 5      $��Discount��6)   $		 150��6;   
 6G   �� *
Discount+�� 5      $��Discount��6)   $	 0��6;   
 6E   �� *	OrderId+�� 5      $��Discount��6)   $	 0��6;   
 6E   �� *	ProductId+5      5      �� 5      $ 
Horizontal=�6   $��0,8,0,0q�62   .+�5      �� -   5         ��Name$ btnAdd��5      $	��Add��6)   $	��150��67   $��45,0,0,0q�6C   $��30��6U   6a   �� 5      -   5        $ 	btnUpdate��5      $��Update��6,   $	��150��6=   $��25,0,0,0q�6I   6[   �� 5      -   5        $ 	btnDelete��5      $��Delete��6,   $	��150��6=   $��25,0,0,0q�6I   6[   �� 5      -   5        $ 	btnCancel��5      $��Cancel��6,   $	��150��6=   $��25,0,0,0q�6I   6[   5       5!      5"      5#      