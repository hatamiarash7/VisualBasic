Public Class Theme
    Public _BLACK As Color
    Public _WHITE As Color
    Public _RED As Color
    Public _BLUE As Color
    Public _GREEN As Color
    Public _ROYALBLUE As Color
    Public _ROYALBLUE_DARK As Color
    Public _GRAY As Color
    Public _GOLD As Color
    Public _YELLOW As Color
    Public Sub New()
        _BLACK = Color.FromArgb(0, 0, 0)
        _WHITE = Color.FromArgb(255, 255, 255)
        _RED = Color.FromArgb(230, 0, 5)
        _BLUE = Color.FromArgb(34, 39, 255)
        _GREEN = Color.FromArgb(0, 180, 0)
        _ROYALBLUE = Color.FromArgb(41, 63, 90)
        _ROYALBLUE_DARK = Color.FromArgb(34, 52, 74)
        _GRAY = Color.FromArgb(220, 220, 220)
        _GOLD = Color.FromArgb(255, 231, 55)
        _YELLOW = Color.FromArgb(255, 255, 0)
    End Sub
    Public Sub SetLightTheme(ctrl As Control)
        ctrl.BackColor = _WHITE
        If (TypeOf ctrl Is MenuStrip) Then ctrl.BackColor = _GRAY
        ctrl.ForeColor = _ROYALBLUE
    End Sub
    Public Sub SetDarkTheme(ctrl As Control)
        ctrl.BackColor = _ROYALBLUE
        If (TypeOf ctrl Is MenuStrip) Then ctrl.BackColor = _ROYALBLUE_DARK
        ctrl.ForeColor = _GOLD
    End Sub
End Class