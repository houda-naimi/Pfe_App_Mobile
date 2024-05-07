package crc6450167e81b80087c8;


public class CollectionViewRenderer
	extends crc643f46942d9dd1fff9.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"";
		mono.android.Runtime.register ("Sharpnado.CollectionView.Droid.Renderers.CollectionViewRenderer, Sharpnado.CollectionView.Droid", CollectionViewRenderer.class, __md_methods);
	}


	public CollectionViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CollectionViewRenderer.class)
			mono.android.TypeManager.Activate ("Sharpnado.CollectionView.Droid.Renderers.CollectionViewRenderer, Sharpnado.CollectionView.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public CollectionViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CollectionViewRenderer.class)
			mono.android.TypeManager.Activate ("Sharpnado.CollectionView.Droid.Renderers.CollectionViewRenderer, Sharpnado.CollectionView.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CollectionViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CollectionViewRenderer.class)
			mono.android.TypeManager.Activate ("Sharpnado.CollectionView.Droid.Renderers.CollectionViewRenderer, Sharpnado.CollectionView.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);

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
