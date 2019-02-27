using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomBox<T> : ImmutableObject
{
    protected class RandomItem
    {
        public int Cursor { get; set; }
        public int Ratio { get; set; }
        public T Item { get; set; }
    }
    protected Dictionary<int, RandomItem> _list = new Dictionary<int, RandomItem>();

    int[] _keys = null;
    int _ratioCursor = 0;
    protected T _last;

    public RandomBox() { }

    public void Load<X>(List<X> items) where X : T, IRandomBoxItem
    {
        this._list.Clear();
        this._keys = null;
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            this.Add(item.Ratio, item);
        }
    }

    public virtual T Pick()
    {
        int cursor = this.GetCursor(this._ratioCursor);

        if (this._keys == null)
        {
            this._keys = this._list.Keys.ToArray();
            this._last = this._list.Last().Value.Item;
        }

        for (int i = 0, t = _keys.Length; i < t; i++)
        {
            int key = _keys[i];
            if (cursor < key)
                return this._list[key].Item;
        }

        return this._last; ;
    }

    public bool Add(int Ratio, T Value)
    {
        this.SetImmutable();

        this._ratioCursor += Ratio;
        RandomItem item = new RandomItem()
        {
            Ratio = Ratio,
            Item = Value,
            Cursor = this._ratioCursor
        };
        _list.Add(this._ratioCursor, item);
        return true;
    }

    protected virtual int GetCursor(int max)
    {
        return new Random().Next(0, max);
    }
}

public interface IRandomBoxItem
{
    int Ratio { get; }
}

public class ImmutableObject
{
    protected bool IsComplete = false;
    public virtual void Complete()
    {
        this.IsComplete = true;
    }

    public void SetImmutable()
    {
        if (this.IsComplete == true)
            throw new Exception(string.Format("{0} is completed object", this.GetType().Name));
    }
}
