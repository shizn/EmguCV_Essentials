#include "MyForm.h"

using namespace CppHelloWorld;

int main(array<System::String^>^ args)  
{
	MyForm ^ f = gcnew MyForm;
	Application::Run(f);
	return 0;
}
