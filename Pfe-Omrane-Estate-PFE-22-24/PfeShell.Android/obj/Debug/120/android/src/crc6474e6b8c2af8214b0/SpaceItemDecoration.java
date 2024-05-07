package crc6474e6b8c2af8214b0;


public class SpaceItemDecoration
	extends androidx.recyclerview.widget.RecyclerView.ItemDecoration
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemOffsets:(Landroid/graphics/Rect;Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$State;)V:GetGetItemOffsets_Landroid_graphics_Rect_Landroid_view_View_Landroidx_recyclerview_widget_RecyclerView_Landroidx_recyclerview_widget_RecyclerView_State_Handler\n" +
			"";
		mono.android.Runtime.register ("Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList.SpaceItemDecoration, Sharpnado.HorizontalListView.Droid", SpaceItemDecoration.class, __md_methods);
	}


	public SpaceItemDecoration ()
	{
		super ();
		if (getClass () == SpaceItemDecoration.class)
			mono.android.TypeManager.Activate ("Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList.SpaceItemDecoration, Sharpnado.HorizontalListView.Droid", "", this, new java.lang.Object[] {  });
	}

	public SpaceItemDecoration (int p0)
	{
		super ();
		if (getClass () == SpaceItemDecoration.class)
			mono.android.TypeManager.Activate ("Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList.SpaceItemDecoration, Sharpnado.HorizontalListView.Droid", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void getItemOffsets (android.graphics.Rect p0, android.view.View p1, androidx.recyclerview.widget.RecyclerView p2, androidx.recyclerview.widget.RecyclerView.State p3)
	{
		n_getItemOffsets (p0, p1, p2, p3);
	}

	private native void n_getItemOffsets (android.graphics.Rect p0, android.view.View p1, androidx.recyclerview.widget.RecyclerView p2, androidx.recyclerview.widget.RecyclerView.State p3);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
