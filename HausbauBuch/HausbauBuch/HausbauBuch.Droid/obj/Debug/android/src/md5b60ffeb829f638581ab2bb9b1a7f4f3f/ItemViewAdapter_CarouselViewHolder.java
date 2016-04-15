package md5b60ffeb829f638581ab2bb9b1a7f4f3f;


public class ItemViewAdapter_CarouselViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Platform.Android.ItemViewAdapter+CarouselViewHolder, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", ItemViewAdapter_CarouselViewHolder.class, __md_methods);
	}


	public ItemViewAdapter_CarouselViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ItemViewAdapter_CarouselViewHolder.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.ItemViewAdapter+CarouselViewHolder, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	java.util.ArrayList refList;
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
