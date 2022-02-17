using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mux.Model
{
	public class Category : INotifyPropertyChanged
	{
		private int _id;
		private string _name;

		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public List<Product> Products { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;

		public Category()
		{
			Id = 0;
			Name = "";
		}
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
