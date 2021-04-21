using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
[RealtimeModel]
public partial class color_Model 
{
    //realtime model to keep track color, Name, velocity.
    [RealtimeProperty(1, true, true)]
    private Color _pColor;
    [RealtimeProperty(2, true, true)]
    private string _pName;

    [RealtimeProperty(3, true, false)]
    private Vector3 _pVelocity;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class color_Model : RealtimeModel {
    public UnityEngine.Color pColor {
        get {
            return _cache.LookForValueInCache(_pColor, entry => entry.pColorSet, entry => entry.pColor);
        }
        set {
            if (this.pColor == value) return;
            _cache.UpdateLocalCache(entry => { entry.pColorSet = true; entry.pColor = value; return entry; });
            InvalidateReliableLength();
            FirePColorDidChange(value);
        }
    }
    
    public string pName {
        get {
            return _cache.LookForValueInCache(_pName, entry => entry.pNameSet, entry => entry.pName);
        }
        set {
            if (this.pName == value) return;
            _cache.UpdateLocalCache(entry => { entry.pNameSet = true; entry.pName = value; return entry; });
            InvalidateReliableLength();
            FirePNameDidChange(value);
        }
    }
    
    public int pScore {
        get {
            return _cache.LookForValueInCache(_pScore, entry => entry.pScoreSet, entry => entry.pScore);
        }
        set {
            if (this.pScore == value) return;
            _cache.UpdateLocalCache(entry => { entry.pScoreSet = true; entry.pScore = value; return entry; });
            InvalidateReliableLength();
            FirePScoreDidChange(value);
        }
    }
    
    public UnityEngine.Vector3 pVelocity {
        get {
            return _cache.LookForValueInCache(_pVelocity, entry => entry.pVelocitySet, entry => entry.pVelocity);
        }
        set {
            if (this.pVelocity == value) return;
            _cache.UpdateLocalCache(entry => { entry.pVelocitySet = true; entry.pVelocity = value; return entry; });
            InvalidateReliableLength();
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(color_Model model, T value);
    public event PropertyChangedHandler<UnityEngine.Color> pColorDidChange;
    public event PropertyChangedHandler<string> pNameDidChange;
    public event PropertyChangedHandler<int> pScoreDidChange;
    
    private struct LocalCacheEntry {
        public bool pColorSet;
        public UnityEngine.Color pColor;
        public bool pNameSet;
        public string pName;
        public bool pScoreSet;
        public int pScore;
        public bool pVelocitySet;
        public UnityEngine.Vector3 pVelocity;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache = new LocalChangeCache<LocalCacheEntry>();
    
    public enum PropertyID : uint {
        PColor = 1,
        PName = 2,
        PScore = 3,
        PVelocity = 4,
    }
    
    public color_Model() : this(null) {
    }
    
    public color_Model(RealtimeModel parent) : base(null, parent) {
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        UnsubscribeClearCacheCallback();
    }
    
    private void FirePColorDidChange(UnityEngine.Color value) {
        try {
            pColorDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePNameDidChange(string value) {
        try {
            pNameDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePScoreDidChange(int value) {
        try {
            pScoreDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        int length = 0;
        if (context.fullModel) {
            FlattenCache();
            length += WriteStream.WriteBytesLength((uint)PropertyID.PColor, WriteStream.ColorToBytesLength());
            length += WriteStream.WriteStringLength((uint)PropertyID.PName, _pName);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.PScore, (uint)_pScore);
            length += WriteStream.WriteBytesLength((uint)PropertyID.PVelocity, WriteStream.Vector3ToBytesLength());
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.pColorSet) {
                length += WriteStream.WriteBytesLength((uint)PropertyID.PColor, WriteStream.ColorToBytesLength());
            }
            if (entry.pNameSet) {
                length += WriteStream.WriteStringLength((uint)PropertyID.PName, entry.pName);
            }
            if (entry.pScoreSet) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.PScore, (uint)entry.pScore);
            }
            if (entry.pVelocitySet) {
                length += WriteStream.WriteBytesLength((uint)PropertyID.PVelocity, WriteStream.Vector3ToBytesLength());
            }
        }
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var didWriteProperties = false;
        
        if (context.fullModel) {
            stream.WriteBytes((uint)PropertyID.PColor, WriteStream.ColorToBytes(_pColor));
            stream.WriteString((uint)PropertyID.PName, _pName);
            stream.WriteVarint32((uint)PropertyID.PScore, (uint)_pScore);
            stream.WriteBytes((uint)PropertyID.PVelocity, WriteStream.Vector3ToBytes(_pVelocity));
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.pColorSet || entry.pNameSet || entry.pScoreSet || entry.pVelocitySet) {
                _cache.PushLocalCacheToInflight(context.updateID);
                ClearCacheOnStreamCallback(context);
            }
            if (entry.pColorSet) {
                stream.WriteBytes((uint)PropertyID.PColor, WriteStream.ColorToBytes(entry.pColor));
                didWriteProperties = true;
            }
            if (entry.pNameSet) {
                stream.WriteString((uint)PropertyID.PName, entry.pName);
                didWriteProperties = true;
            }
            if (entry.pScoreSet) {
                stream.WriteVarint32((uint)PropertyID.PScore, (uint)entry.pScore);
                didWriteProperties = true;
            }
            if (entry.pVelocitySet) {
                stream.WriteBytes((uint)PropertyID.PVelocity, WriteStream.Vector3ToBytes(entry.pVelocity));
                didWriteProperties = true;
            }
            
            if (didWriteProperties) InvalidateReliableLength();
        }
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.PColor: {
                    UnityEngine.Color previousValue = _pColor;
                    _pColor = ReadStream.ColorFromBytes(stream.ReadBytes());
                    bool pColorExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.pColorSet);
                    if (!pColorExistsInChangeCache && _pColor != previousValue) {
                        FirePColorDidChange(_pColor);
                    }
                    break;
                }
                case (uint)PropertyID.PName: {
                    string previousValue = _pName;
                    _pName = stream.ReadString();
                    bool pNameExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.pNameSet);
                    if (!pNameExistsInChangeCache && _pName != previousValue) {
                        FirePNameDidChange(_pName);
                    }
                    break;
                }
                case (uint)PropertyID.PScore: {
                    int previousValue = _pScore;
                    _pScore = (int)stream.ReadVarint32();
                    bool pScoreExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.pScoreSet);
                    if (!pScoreExistsInChangeCache && _pScore != previousValue) {
                        FirePScoreDidChange(_pScore);
                    }
                    break;
                }
                case (uint)PropertyID.PVelocity: {
                    _pVelocity = ReadStream.Vector3FromBytes(stream.ReadBytes());
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
        }
    }
    
    #region Cache Operations
    
    private StreamEventDispatcher _streamEventDispatcher;
    
    private void FlattenCache() {
        _pColor = pColor;
        _pName = pName;
        _pScore = pScore;
        _pVelocity = pVelocity;
        _cache.Clear();
    }
    
    private void ClearCache(uint updateID) {
        _cache.RemoveUpdateFromInflight(updateID);
    }
    
    private void ClearCacheOnStreamCallback(StreamContext context) {
        if (_streamEventDispatcher != context.dispatcher) {
            UnsubscribeClearCacheCallback(); // unsub from previous dispatcher
        }
        _streamEventDispatcher = context.dispatcher;
        _streamEventDispatcher.AddStreamCallback(context.updateID, ClearCache);
    }
    
    private void UnsubscribeClearCacheCallback() {
        if (_streamEventDispatcher != null) {
            _streamEventDispatcher.RemoveStreamCallback(ClearCache);
            _streamEventDispatcher = null;
        }
    }
    
    #endregion
}
/* ----- End Normal Autogenerated Code ----- */
