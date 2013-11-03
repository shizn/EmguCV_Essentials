#pragma once

namespace HelloWorldCpp {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace Emgu::CV;
	using namespace Emgu::CV::CvEnum;
	using namespace Emgu::CV::Structure;

	/// <summary>
	/// Summary for MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^  button1;
	protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(135, 57);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(111, 23);
			this->button1->TabIndex = 0;
			this->button1->Text = L"Hello World";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(377, 138);
			this->Controls->Add(this->button1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
				 String ^ winname = "Fisrt Cpp Window";
				 //Create a window with the specific name
				 CvInvoke::cvNamedWindow(winname);
				 //Create an image of 480x200 with color yellow
				 Emgu::CV::Image<Bgr,Byte>^ img1 = gcnew Emgu::CV::Image<Bgr,Byte>(480, 200, Bgr(0, 255, 255));
				 
				 //Create a font
				 MCvFont font = MCvFont(Emgu::CV::CvEnum::FONT::CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
				 //Draw "Hello, world" on the yellow image; Start point is (25, 100) with color blue				 
				 String ^ message = "Hello World";
				 img1->Draw(message,font,Point(25,100),Bgr(255,0,0));
				 //Show the img1 in the window
				 CvInvoke::cvShowImage(winname,img1);
				 //A key pressing event
				 CvInvoke::cvWaitKey(0);
                 //Destroy the window
				 CvInvoke::cvDestroyWindow(winname);
			 }
	};
}
