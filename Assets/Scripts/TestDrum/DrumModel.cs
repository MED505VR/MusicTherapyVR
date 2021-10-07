using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class DrumModel
{
    [RealtimeProperty(1, true, true)] private bool _hit;

    [RealtimeProperty(2, true, true)] private float _volume;
}


/* ----- Begin Normal Autogenerated Code ----- */
public partial class DrumModel : RealtimeModel
{
    public bool hit
    {
        get { return _cache.LookForValueInCache(_hit, entry => entry.hitSet, entry => entry.hit); }
        set
        {
            if (hit == value) return;
            _cache.UpdateLocalCache(entry =>
            {
                entry.hitSet = true;
                entry.hit = value;
                return entry;
            });
            InvalidateReliableLength();
            FireHitDidChange(value);
        }
    }

    public float volume
    {
        get { return _cache.LookForValueInCache(_volume, entry => entry.volumeSet, entry => entry.volume); }
        set
        {
            if (volume == value) return;
            _cache.UpdateLocalCache(entry =>
            {
                entry.volumeSet = true;
                entry.volume = value;
                return entry;
            });
            InvalidateReliableLength();
            FireVolumeDidChange(value);
        }
    }

    public delegate void PropertyChangedHandler<in T>(DrumModel model, T value);

    public event PropertyChangedHandler<bool> hitDidChange;
    public event PropertyChangedHandler<float> volumeDidChange;

    private struct LocalCacheEntry
    {
        public bool hitSet;
        public bool hit;
        public bool volumeSet;
        public float volume;
    }

    private LocalChangeCache<LocalCacheEntry> _cache = new LocalChangeCache<LocalCacheEntry>();

    public enum PropertyID : uint
    {
        Hit = 1,
        Volume = 2
    }

    public DrumModel() : this(null)
    {
    }

    public DrumModel(RealtimeModel parent) : base(null, parent)
    {
    }

    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent)
    {
        UnsubscribeClearCacheCallback();
    }

    private void FireHitDidChange(bool value)
    {
        try
        {
            hitDidChange?.Invoke(this, value);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void FireVolumeDidChange(float value)
    {
        try
        {
            volumeDidChange?.Invoke(this, value);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    protected override int WriteLength(StreamContext context)
    {
        var length = 0;
        if (context.fullModel)
        {
            FlattenCache();
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Hit, _hit ? 1u : 0u);
            length += WriteStream.WriteFloatLength((uint)PropertyID.Volume);
        }
        else if (context.reliableChannel)
        {
            var entry = _cache.localCache;
            if (entry.hitSet) length += WriteStream.WriteVarint32Length((uint)PropertyID.Hit, entry.hit ? 1u : 0u);
            if (entry.volumeSet) length += WriteStream.WriteFloatLength((uint)PropertyID.Volume);
        }

        return length;
    }

    protected override void Write(WriteStream stream, StreamContext context)
    {
        var didWriteProperties = false;

        if (context.fullModel)
        {
            stream.WriteVarint32((uint)PropertyID.Hit, _hit ? 1u : 0u);
            stream.WriteFloat((uint)PropertyID.Volume, _volume);
        }
        else if (context.reliableChannel)
        {
            var entry = _cache.localCache;
            if (entry.hitSet || entry.volumeSet)
            {
                _cache.PushLocalCacheToInflight(context.updateID);
                ClearCacheOnStreamCallback(context);
            }

            if (entry.hitSet)
            {
                stream.WriteVarint32((uint)PropertyID.Hit, entry.hit ? 1u : 0u);
                didWriteProperties = true;
            }

            if (entry.volumeSet)
            {
                stream.WriteFloat((uint)PropertyID.Volume, entry.volume);
                didWriteProperties = true;
            }

            if (didWriteProperties) InvalidateReliableLength();
        }
    }

    protected override void Read(ReadStream stream, StreamContext context)
    {
        while (stream.ReadNextPropertyID(out var propertyID))
            switch (propertyID)
            {
                case (uint)PropertyID.Hit:
                {
                    var previousValue = _hit;
                    _hit = stream.ReadVarint32() != 0;
                    var hitExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hitSet);
                    if (!hitExistsInChangeCache && _hit != previousValue) FireHitDidChange(_hit);
                    break;
                }
                case (uint)PropertyID.Volume:
                {
                    var previousValue = _volume;
                    _volume = stream.ReadFloat();
                    var volumeExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.volumeSet);
                    if (!volumeExistsInChangeCache && _volume != previousValue) FireVolumeDidChange(_volume);
                    break;
                }
                default:
                {
                    stream.SkipProperty();
                    break;
                }
            }
    }

    #region Cache Operations

    private StreamEventDispatcher _streamEventDispatcher;

    private void FlattenCache()
    {
        _hit = hit;
        _volume = volume;
        _cache.Clear();
    }

    private void ClearCache(uint updateID)
    {
        _cache.RemoveUpdateFromInflight(updateID);
    }

    private void ClearCacheOnStreamCallback(StreamContext context)
    {
        if (_streamEventDispatcher != context.dispatcher)
            UnsubscribeClearCacheCallback(); // unsub from previous dispatcher
        _streamEventDispatcher = context.dispatcher;
        _streamEventDispatcher.AddStreamCallback(context.updateID, ClearCache);
    }

    private void UnsubscribeClearCacheCallback()
    {
        if (_streamEventDispatcher != null)
        {
            _streamEventDispatcher.RemoveStreamCallback(ClearCache);
            _streamEventDispatcher = null;
        }
    }

    #endregion
}
/* ----- End Normal Autogenerated Code ----- */