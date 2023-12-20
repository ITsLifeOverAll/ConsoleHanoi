namespace ConsoleHanoi;

enum State
{
	ChooseSource,
	ChooseTarget,
	InvalidTarget,

	Win,
	Abort,
	Restart,
}