namespace UIEditor.Controls
{
    /// <summary>
    /// Specifies constants defining how to dock the buttons in a <see cref="DialogForm"/>
    /// </summary>
    public enum DialogFormButtonDock
    {
        Bottom,
        Right
    }

    /// <summary>
    /// Specifies constants defining the type of <see cref="ExplorerTreeNode"/>
    /// </summary>
    public enum ExplorerTreeNodeType
    {
        Directory,
        File,
        Standard,
    }

    /// <summary>
    /// Specifies constants defining how to align a marker
    /// </summary>
    public enum MarkerAlign
    {
        Top,
        Bottom
    }

    /// <summary>
    /// Specifies constants defining available border styles for a <see cref="RoundedForm"/>
    /// </summary>
    public enum RoundedFormBorderStyle
    {
        None,
        Raised,
        Single
    }

    /// <summary>
    /// Specifies constants defining how to wrap text
    /// </summary>
    public enum WordWrap
    {
        NoWrap,
        WrapToControl,
        WrapToPrintDocument,
    }
}
