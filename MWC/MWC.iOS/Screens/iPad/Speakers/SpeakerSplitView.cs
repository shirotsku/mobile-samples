using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using MWC.iOS.Screens.iPhone.Speakers;

namespace MWC.iOS.Screens.iPad.Speakers
{
	public class SpeakerSplitView : UISplitViewController
	{
		SpeakersScreen _speakersList;
		SpeakerSessionMasterDetail _speakerDetailWithSession;
		
		public SpeakerSplitView ()
		{
			//View.Bounds = new RectangleF(0,0,UIScreen.MainScreen.Bounds.Width,UIScreen.MainScreen.Bounds.Height);
			Delegate = new SpeakerSplitViewDelegate();
			
			_speakersList = new SpeakersScreen(this);
			_speakerDetailWithSession = new SpeakerSessionMasterDetail(-1);
			
			this.ViewControllers = new UIViewController[]
				{_speakersList, _speakerDetailWithSession};
		}
		
		public void ShowSpeaker (int speakerID)
		{
			_speakerDetailWithSession = this.ViewControllers[1] as SpeakerSessionMasterDetail;
			_speakerDetailWithSession.Update(speakerID);
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
	}

 	public class SpeakerSplitViewDelegate : UISplitViewControllerDelegate
    {
		public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
		{
			return inOrientation == UIInterfaceOrientation.Portrait
				|| inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
		}

		public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
		{
			SpeakerSessionMasterDetail dvc = svc.ViewControllers[1] as SpeakerSessionMasterDetail;
			
			if (dvc != null) {
				dvc.AddNavBarButton (barButtonItem);
				dvc.Popover = pc;
			}
		}
		
		public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
		{
			SpeakerSessionMasterDetail dvc = svc.ViewControllers[1] as SpeakerSessionMasterDetail;
			
			if (dvc != null) {
				dvc.RemoveNavBarButton ();
				dvc.Popover = null;
			}
		}
	}
}