package crc6474e6b8c2af8214b0;


public class AndroidHorizontalListViewRenderer_ViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList.AndroidHorizontalListViewRenderer+ViewHolder, Sharpnado.HorizontalListView.Droid", AndroidHorizontalListViewRenderer_ViewHolder.class, __md_methods);
	}


	public AndroidHorizontalListViewRenderer_ViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == AndroidHorizontalListViewRenderer_ViewHolder.class)
			mono.android.TypeManager.Activate ("Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList.AndroidHorizontalListViewRenderer+ViewHolder, Sharpnado.HorizontalListView.Droid", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
