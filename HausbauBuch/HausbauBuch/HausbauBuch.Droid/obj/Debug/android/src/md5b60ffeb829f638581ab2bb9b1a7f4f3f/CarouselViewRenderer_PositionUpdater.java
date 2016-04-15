package md5b60ffeb829f638581ab2bb9b1a7f4f3f;


public class CarouselViewRenderer_PositionUpdater
	extends android.support.v7.widget.RecyclerView.AdapterDataObserver
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onItemRangeInserted:(II)V:GetOnItemRangeInserted_IIHandler\n" +
			"n_onItemRangeRemoved:(II)V:GetOnItemRangeRemoved_IIHandler\n" +
			"n_onItemRangeMoved:(III)V:GetOnItemRangeMoved_IIIHandler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Platform.Android.CarouselViewRenderer+PositionUpdater, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", CarouselViewRenderer_PositionUpdater.class, __md_methods);
	}


	public CarouselViewRenderer_PositionUpdater () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CarouselViewRenderer_PositionUpdater.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.CarouselViewRenderer+PositionUpdater, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CarouselViewRenderer_PositionUpdater (md5b60ffeb829f638581ab2bb9b1a7f4f3f.CarouselViewRenderer p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == CarouselViewRenderer_PositionUpdater.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.CarouselViewRenderer+PositionUpdater, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", "Xamarin.Forms.Platform.Android.CarouselViewRenderer, Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onItemRangeInserted (int p0, int p1)
	{
		n_onItemRangeInserted (p0, p1);
	}

	private native void n_onItemRangeInserted (int p0, int p1);


	public void onItemRangeRemoved (int p0, int p1)
	{
		n_onItemRangeRemoved (p0, p1);
	}

	private native void n_onItemRangeRemoved (int p0, int p1);


	public void onItemRangeMoved (int p0, int p1, int p2)
	{
		n_onItemRangeMoved (p0, p1, p2);
	}

	private native void n_onItemRangeMoved (int p0, int p1, int p2);

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
