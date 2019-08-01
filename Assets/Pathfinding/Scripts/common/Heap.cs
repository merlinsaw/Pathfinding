//-------------------------------------------------------
//	https://www.youtube.com/watch?v=3Dw5d7PlcTM&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=4
//-------------------------------------------------------

#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

#endregion

namespace Common.Heap
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Heap<T> where T : IHeapItem<T>
  {
	T[] items;
	int currentItemCount;

	public Heap(int maxHeapSize)
	{
	  items = new T[maxHeapSize];
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="item"></param>
	public void Add(T item)
	{
	  item.HeapIndex = currentItemCount;
	  items[currentItemCount] = item;
	  SortUp(item);
	  currentItemCount++;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public T RemoveFirst()
	{
	  T firstItem = items[0];
	  currentItemCount--;
	  items[0] = items[currentItemCount];
	  items[0].HeapIndex = 0;
	  SortDown(items[0]);
	  return firstItem;
	}

	/// <summary>
	/// 
	/// </summary>
	public void UpdateItem(T item)
	{
	  SortUp(item);
	}

	/// <summary>
	/// 
	/// </summary>
	public int Count
	{
	  get
	  {
		return currentItemCount;
	  }
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Contains(T item)
	{
	  return Equals(items[item.HeapIndex], item);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="item"></param>
	void SortDown(T item)
	{
	  while (true)
	  {
		int childIndexLeft = item.HeapIndex * 2 + 1;
		int childIndexRight = item.HeapIndex * 2 + 2;
		int swapIndex = 0;

		if (childIndexLeft < currentItemCount)
		{
		  swapIndex = childIndexLeft;

		  if (childIndexRight < currentItemCount)
		  {
			if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0 )
			{
			  swapIndex = childIndexRight;
			}
		  }

		  if (item.CompareTo(items[swapIndex]) < 0 )
		  {
			Swap(item,items[swapIndex]);
		  }
		  else
		  {
			return;
		  }
		}
		else
		{
		  return;
		}
	  }
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="item"></param>
	void SortUp(T item)
	{
	  int parentIndex = (item.HeapIndex - 1) / 2;

	  while (true)
	  {
		T parentItem = items[parentIndex];
		if (item.CompareTo(parentItem) > 0)
		{
		  Swap(item, parentItem);
		}
		else
		{
		  break;
		}

		parentIndex = (item.HeapIndex - 1) / 2;
	  }
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="itemA"></param>
	/// <param name="itemB"></param>
	void Swap(T itemA, T itemB)
	{
	  items[itemA.HeapIndex] = itemB;
	  items[itemB.HeapIndex] = itemA;
	  int itemAIndex = itemA.HeapIndex;
	  itemA.HeapIndex = itemB.HeapIndex;
	  itemB.HeapIndex = itemAIndex;
	}

  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IHeapItem<T> : IComparable<T>
  {
	int HeapIndex
	{
	  get;
	  set;
	}
  }
}
