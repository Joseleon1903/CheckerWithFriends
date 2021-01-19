using System;

namespace Assets.Scripts.General
{
	public interface IHeapItem<T> : IComparable<T>
	{
		int HeapIndex
		{
			get;
			set;
		}
	}

}
