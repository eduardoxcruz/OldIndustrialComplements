using System.Collections.Generic;
using System.ComponentModel;

namespace Mux.Model
{
	public class Employee : INotifyPropertyChanged
	{
#pragma warning disable 8632
		public event PropertyChangedEventHandler? PropertyChanged;
		
		private int _id;
		private string? _type;
		private string _fullName;
		private string? _password;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public string? Type
		{
			get => _type;
			set
			{
				_type = value;
				OnPropertyChanged(nameof(Type));
			}
		}
		public string FullName
		{
			get => _fullName;
			set
			{
				_fullName = value;
				OnPropertyChanged(nameof(FullName));
			}
		}
		public string? Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
		public List<ProductToBuy> ShoppingCart { get; set; }
		public List<ProductRequest> ProductRequests { get; set; }
		public List<ProductChangeLog> ProductChangeLogs { get; set; }
#pragma warning restore 8632
		
		public Employee()
		{
			Id = 0;
			Type = "";
			FullName = "";
			Password = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
