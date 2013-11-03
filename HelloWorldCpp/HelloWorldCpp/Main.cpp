#include <Windows.h>
#include "MyForm.h"

using namespace MyForm;
using namespace System::Windows::Forms;

int main(array<System::String^>^ args)  
{
	MyForm ^ f = gcnew MyForm;
	Application::Run(f);
	
}