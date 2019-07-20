using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.Extensions
{

    public class GUIValuesScope : GUI.Scope
    {
        private Values oldValues;

        public GUIValuesScope()
        {
            oldValues = new Values()
            {
                color = GUI.color,
                contentColor = GUI.contentColor,
                backgroundColor = GUI.backgroundColor,
                matrix = GUI.matrix,
                depth = GUI.depth,
                enabled = GUI.enabled,
                tooltip = GUI.tooltip,
            };
        }



        protected override void CloseScope()
        {
            GUI.color = oldValues.color;
            GUI.contentColor = oldValues.contentColor;
            GUI.backgroundColor = oldValues.backgroundColor;
            GUI.matrix = oldValues.matrix;
            GUI.depth = oldValues.depth;
            GUI.enabled = oldValues.enabled;
            GUI.tooltip = oldValues.tooltip;
        }

        struct Values
        {
            public Color color;
            public Color contentColor;
            public Color backgroundColor;
            public bool enabled;
            public Matrix4x4 matrix;
            public int depth;
            public string tooltip;
        }

    }


    //public abstract class GUIValueScope<T> : UnityEngine.GUI.Scope
    //{
    //    private T oldValue;
    //    public GUIValueScope(T newValue)
    //    {
    //        oldValue = Value;
    //        Value = newValue;
    //    }

    //    protected abstract T Value { get; set; }

    //    protected override void CloseScope()
    //    {
    //        Value = oldValue;
    //    }
    //}


    ///// <summary>
    ///// <see cref="GUI.enabled"/>
    ///// </summary>
    //public class GUIEnabledScope : GUIValueScope<bool>
    //{
    //    public GUIEnabledScope(bool enabled)
    //        : base(enabled)
    //    {
    //    }

    //    protected override bool Value
    //    {
    //        get { return GUI.enabled; }
    //        set { GUI.enabled = value; }
    //    }
    //}


    ///// <summary>
    ///// <see cref="GUI.matrix"/>
    ///// </summary>
    //public class GUIMatrixScope : GUIValueScope<Matrix4x4>
    //{
    //    public GUIMatrixScope(Matrix4x4 matrix)
    //        : base(matrix)
    //    {
    //    }

    //    protected override Matrix4x4 Value
    //    {
    //        get { return GUI.matrix; }
    //        set { GUI.matrix = value; }
    //    }
    //}
    ///// <summary>
    ///// <see cref="GUI.color"/>
    ///// </summary>
    //public class GUIColorScope : GUIValueScope<Color>
    //{
    //    public GUIColorScope(Color color)
    //        : base(color)
    //    {
    //    }

    //    protected override Color Value
    //    {
    //        get { return GUI.color; }
    //        set { GUI.color = value; }
    //    }
    //}

    ///// <summary>
    ///// <see cref="GUI.contentColor"/>
    ///// </summary>
    //public class GUIContentColorScope : GUIValueScope<Color>
    //{
    //    public GUIContentColorScope(Color color)
    //        : base(color)
    //    {
    //    }

    //    protected override Color Value
    //    {
    //        get { return GUI.contentColor; }
    //        set { GUI.contentColor = value; }
    //    }
    //}
    ///// <summary>
    ///// <see cref="GUI.backgroundColor"/>
    ///// </summary>
    //public class GUIBackgroundColorScope : GUIValueScope<Color>
    //{
    //    public GUIBackgroundColorScope(Color color)
    //        : base(color)
    //    {
    //    }

    //    protected override Color Value
    //    {
    //        get { return GUI.backgroundColor; }
    //        set { GUI.backgroundColor = value; }
    //    }
    //}
    ///// <summary>
    ///// <see cref="GUI.color"/>
    ///// </summary>
    //public class GUIDepthScope : GUIValueScope<int>
    //{
    //    public GUIDepthScope(int depth)
    //        : base(depth)
    //    {
    //    }

    //    protected override int Value
    //    {
    //        get { return GUI.depth; }
    //        set { GUI.depth = value; }
    //    }
    //}
}
