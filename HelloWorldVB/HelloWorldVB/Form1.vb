Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure

Public Class Form1
    'Set the name of pop-up window
    Dim winname = "First VB Window"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Create a window with the specific name
        CvInvoke.cvNamedWindow(winname)

        'Create an image of 480x200 with color yellow
        Using img As Image(Of Bgr, Byte) = New Image(Of Bgr, Byte)(480, 200, New Bgr(0, 255, 255))

            'Create a font
            Dim font = New MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0)
            '//Draw "Hello, world VB" on the yellow image; Start point is (25, 100) with color blue
            img.Draw("Hello, world VB", font, New Point(25, 100), New Bgr(255, 0, 0))

            'Show the img1 in the window
            CvInvoke.cvShowImage(winname, img.Ptr)
            'A key pressing event
            CvInvoke.cvWaitKey(0)
            'Destroy the window
            CvInvoke.cvDestroyWindow(winname)
        End Using
    End Sub
End Class

