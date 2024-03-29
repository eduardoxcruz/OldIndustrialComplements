﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mux.Model
{
	public class EncapsulationType : INotifyPropertyChanged
	{
		private int _id;
		private string _name;
		private string _bodyWidth;
		private string _fullDescription;

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
		public string BodyWidth
		{
			get
			{
				return _bodyWidth;
			}
			set
			{
				_bodyWidth = value;
				OnPropertyChanged(nameof(BodyWidth));
			}
		}
		public string FullDescription
		{
			get
			{
				return _fullDescription;
			}
			set
			{
				_fullDescription = value;
				OnPropertyChanged(nameof(FullDescription));
			}
		}
		
		public List<Product> Products { get; set; }

		public EncapsulationType()
		{
			Id = 0;
			Name = "";
			BodyWidth = "";
			FullDescription = "";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
