using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

namespace RoomEnvironment.Models
{
    [RealtimeModel()]
    public partial class ColorChangeLampModel
    {
        [RealtimeProperty(1, true, true)]
        private int _colorIndex;
    }
}


/* ----- Begin Normal Autogenerated Code ----- */
namespace RoomEnvironment.Models {
    public partial class ColorChangeLampModel : RealtimeModel {
        public int colorIndex {
            get {
                return _colorIndexProperty.value;
            }
            set {
                if (_colorIndexProperty.value == value) return;
                _colorIndexProperty.value = value;
                InvalidateReliableLength();
                FireColorIndexDidChange(value);
            }
        }
        
        public delegate void PropertyChangedHandler<in T>(ColorChangeLampModel model, T value);
        public event PropertyChangedHandler<int> colorIndexDidChange;
        
        public enum PropertyID : uint {
            ColorIndex = 1,
        }
        
        #region Properties
        
        private ReliableProperty<int> _colorIndexProperty;
        
        #endregion
        
        public ColorChangeLampModel() : base(null) {
            _colorIndexProperty = new ReliableProperty<int>(1, _colorIndex);
        }
        
        protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
            _colorIndexProperty.UnsubscribeCallback();
        }
        
        private void FireColorIndexDidChange(int value) {
            try {
                colorIndexDidChange?.Invoke(this, value);
            } catch (System.Exception exception) {
                UnityEngine.Debug.LogException(exception);
            }
        }
        
        protected override int WriteLength(StreamContext context) {
            var length = 0;
            length += _colorIndexProperty.WriteLength(context);
            return length;
        }
        
        protected override void Write(WriteStream stream, StreamContext context) {
            var writes = false;
            writes |= _colorIndexProperty.Write(stream, context);
            if (writes) InvalidateContextLength(context);
        }
        
        protected override void Read(ReadStream stream, StreamContext context) {
            var anyPropertiesChanged = false;
            while (stream.ReadNextPropertyID(out uint propertyID)) {
                var changed = false;
                switch (propertyID) {
                    case (uint) PropertyID.ColorIndex: {
                        changed = _colorIndexProperty.Read(stream, context);
                        if (changed) FireColorIndexDidChange(colorIndex);
                        break;
                    }
                    default: {
                        stream.SkipProperty();
                        break;
                    }
                }
                anyPropertiesChanged |= changed;
            }
            if (anyPropertiesChanged) {
                UpdateBackingFields();
            }
        }
        
        private void UpdateBackingFields() {
            _colorIndex = colorIndex;
        }
        
    }
}
/* ----- End Normal Autogenerated Code ----- */
