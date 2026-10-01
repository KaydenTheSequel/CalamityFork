using System;
using System.Collections.Generic;

namespace CalamityMod.DataStructures;

public class HeapDict<TKey, TValue> where TValue : IComparable<TValue>
{
	private readonly Dictionary<TKey, int> _indexMap = new Dictionary<TKey, int>();

	private readonly List<(TKey Key, TValue Value)> _heap = new List<(TKey, TValue)>();

	public int Count => _heap.Count;

	public void Add(TKey key, TValue value)
	{
		if (_indexMap.TryGetValue(key, out var index))
		{
			TValue oldValue = _heap[index].Value;
			_heap[index] = (key, value);
			int cmp = value.CompareTo(oldValue);
			if (cmp < 0)
			{
				HeapifyUp(index);
			}
			else if (cmp > 0)
			{
				HeapifyDown(index);
			}
		}
		else
		{
			_heap.Add((key, value));
			index = _heap.Count - 1;
			_indexMap[key] = index;
			HeapifyUp(index);
		}
	}

	public (TKey, TValue) PopMin()
	{
		if (_heap.Count == 0)
		{
			throw new InvalidOperationException("Heap is empty");
		}
		(TKey, TValue) min = _heap[0];
		List<(TKey Key, TValue Value)> heap = _heap;
		(TKey, TValue) last = heap[heap.Count - 1];
		_heap[0] = last;
		_indexMap[last.Item1] = 0;
		_heap.RemoveAt(_heap.Count - 1);
		_indexMap.Remove(min.Item1);
		if (_heap.Count > 0)
		{
			HeapifyDown(0);
		}
		return min;
	}

	public (TKey, TValue) PeekMin()
	{
		if (_heap.Count == 0)
		{
			throw new InvalidOperationException("Heap is empty");
		}
		return _heap[0];
	}

	private void HeapifyUp(int index)
	{
		while (index > 0)
		{
			int parent = (index - 1) / 2;
			(TKey, TValue) tuple = _heap[index];
			ref TValue item = ref tuple.Item2;
			TValue item2 = _heap[parent].Value;
			if (item.CompareTo(item2) < 0)
			{
				Swap(index, parent);
				index = parent;
				continue;
			}
			break;
		}
	}

	private void HeapifyDown(int index)
	{
		int lastIndex = _heap.Count - 1;
		while (true)
		{
			int left = 2 * index + 1;
			int right = 2 * index + 2;
			int smallest = index;
			if (left <= lastIndex)
			{
				(TKey, TValue) tuple = _heap[left];
				ref TValue item = ref tuple.Item2;
				TValue item2 = _heap[smallest].Value;
				if (item.CompareTo(item2) < 0)
				{
					smallest = left;
				}
			}
			if (right <= lastIndex)
			{
				(TKey, TValue) tuple = _heap[right];
				ref TValue item3 = ref tuple.Item2;
				TValue item4 = _heap[smallest].Value;
				if (item3.CompareTo(item4) < 0)
				{
					smallest = right;
				}
			}
			if (smallest != index)
			{
				Swap(index, smallest);
				index = smallest;
				continue;
			}
			break;
		}
	}

	private void Swap(int i, int j)
	{
		List<(TKey, TValue)> heap = _heap;
		List<(TKey Key, TValue Value)> heap2 = _heap;
		(TKey, TValue) value = _heap[j];
		(TKey, TValue) value2 = _heap[i];
		heap[i] = value;
		heap2[j] = value2;
		_indexMap[_heap[i].Key] = i;
		_indexMap[_heap[j].Key] = j;
	}

	public bool ContainsKey(TKey key)
	{
		return _indexMap.ContainsKey(key);
	}

	public TValue GetValue(TKey key)
	{
		return _heap[_indexMap[key]].Value;
	}
}
