// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "stdafx.h"

#include <stdlib.h>
#include <iostream>
#include <vector>

#include <XnCppWrapper.h>


using namespace std;

static float head[2];

//__declspec(dllexport) int add(int a, int v) {
//	return a + v;
//}


void XN_CALLBACK_TYPE NewUser(xn::UserGenerator& generator,
	XnUserID user,
	void* pCookie)
{
	cout << "New user identified: " << user << endl;
	generator.GetSkeletonCap().RequestCalibration(user, true);
}

// callback function of skeleton: calibration end 
void XN_CALLBACK_TYPE CalibrationEnd(xn::SkeletonCapability& skeleton,
	XnUserID user,
	XnCalibrationStatus eStatus,
	void* pCookie)
{
	cout << "Calibration complete for user " << user << ", ";
	if (eStatus == XN_CALIBRATION_STATUS_OK)
	{
		cout << "Success" << endl;
		skeleton.StartTracking(user);
	}
	else
	{
		cout << "Failure" << endl;
		skeleton.RequestCalibration(user, true);
	}
}



extern "C" __declspec(dllexport)   void Test(float * a) {
	int i = 0;
	while (true) {
		a[0] = i++;
		a[1] = i++;
		a[2] = i++;
	}
	
}
extern "C" __declspec(dllexport)   void GetSk(float * a) {
	a[0] = head[0];
	a[1] = head[1];
	a[2] = head[2];
}


extern "C" __declspec(dllexport)   void Test2(int * a) {

	*a = 1000000;
}


extern "C" __declspec(dllexport)   void GetMySkelenton(float * a) {

	
	

	// 1. initial context
	xn::Context mContext;
	mContext.Init();

	// 2. create user generator
	xn::UserGenerator mUserGenerator;
	mUserGenerator.Create(mContext);
	// 3. Register callback functions of user generator
	XnCallbackHandle hUserCB;
	mUserGenerator.RegisterUserCallbacks(NewUser, NULL, NULL, hUserCB);

	// 4. Register callback functions of skeleton capability
	xn::SkeletonCapability mSC = mUserGenerator.GetSkeletonCap();
	mSC.SetSkeletonProfile(XN_SKEL_PROFILE_ALL);
	XnCallbackHandle hCalibCB;
	mSC.RegisterToCalibrationComplete(CalibrationEnd, &mUserGenerator, hCalibCB);


	// 5. start generate data
	mContext.StartGeneratingAll();
	while (true)
	{
		// 6. Update date
		mContext.WaitAndUpdateAll();

		// 7. get user information
		XnUInt16 nUsers = mUserGenerator.GetNumberOfUsers();
		if (nUsers > 0)
		{

		/*	a[0] = 1111111;
			a[1] = 222222222;
			a[2] = 3333333;
			a[3] = 3000;*/
			// 8. get users
			XnUserID* aUserID = new XnUserID[nUsers];
			mUserGenerator.GetUsers(aUserID, nUsers);

			// 9. check each user
			for (int i = 0; i < nUsers; ++i)
			{
				// 10. if is tracking skeleton
				if (mSC.IsTracking(aUserID[i]))
				{
					// 11. get skeleton joint data
					XnSkeletonJointTransformation mJointTran;
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_HEAD, mJointTran);

					// 12. output information
				    
					a[0] = (float)mJointTran.position.position.X;
					a[1] = (float)mJointTran.position.position.Y;
					a[2] = (float)mJointTran.position.position.Z;
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_LEFT_ELBOW, mJointTran);
					a[3] = (float)mJointTran.position.position.X;
					a[4] = (float)mJointTran.position.position.Y;
					a[5] = (float)mJointTran.position.position.Z;
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_LEFT_HAND, mJointTran);
					a[6] = (float)mJointTran.position.position.X;
					a[7] = (float)mJointTran.position.position.Y;
					a[8] = (float)mJointTran.position.position.Z;
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_RIGHT_ELBOW, mJointTran);
					a[9] = (float)mJointTran.position.position.X;
					a[10] = (float)mJointTran.position.position.Y;
					a[11] = (float)mJointTran.position.position.Z;
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_RIGHT_HAND, mJointTran);
					a[12] = (float)mJointTran.position.position.X;
					a[13] = (float)mJointTran.position.position.Y;
					a[14] = (float)mJointTran.position.position.Z; 
					mSC.GetSkeletonJoint(aUserID[i], XN_SKEL_TORSO, mJointTran);
					a[15] = (float)mJointTran.position.position.X;
					a[16] = (float)mJointTran.position.position.Y;
					a[17] = (float)mJointTran.position.position.Z;
				
				}
			}
			delete[] aUserID;
		}
		if (a[18] == 1.0f) {
			break;
		}
	}
	// 13. stop and shutdown
	mContext.StopGeneratingAll();
	mContext.Release();
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

