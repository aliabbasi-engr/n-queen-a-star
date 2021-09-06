clear
clc

%% Delete previous files
delete stepCount.txt
delete stepCount_b.txt
delete expandedCount.txt
delete expandedCount_b.txt
delete timeElapsed.txt
delete timeElapsed_b.txt
delete output.txt
delete output_b.txt

%% Read test cases from file
fid=fopen('testCases.txt');
tline = fgetl(fid);
testCase = cell(0,1);
while ischar(tline)
    testCase{end+1,1} = tline;
    tline = fgetl(fid);
end
fclose(fid);

%% Solve each test case with both heuristic functions
for index = 1:length(testCase)
    inputFile = fopen('input.txt','w');
    str = string(testCase(index,:));
    fprintf(inputFile, str);
    fclose(inputFile);
    system("AI_Project1.exe");
    system("AI_Project1_b.exe");
end

%% Load reports of application with default heuristic 
stepCount_a = load('stepCount.txt');
expandedCount_a = load('expandedCount.txt');
timeElapsed_a = load('timeElapsed.txt');
s_a = stepCount_a(1:end,2);
e_a = expandedCount_a(1:end,2);
t_a = timeElapsed_a(1:end,2);
k_a = stepCount_a(1:end,1);

%% Load reports of application with proposed heuristic
stepCount_b = load('stepCount_b.txt');
expandedCount_b = load('expandedCount_b.txt');
timeElapsed_b = load('timeElapsed_b.txt');
s_b = stepCount_b(1:end,2);
e_b = expandedCount_b(1:end,2);
t_b = timeElapsed_b(1:end,2);
k_b = stepCount_b(1:end,1);

%% plot the results
subplot(4,1,1);
plot(k_a, s_a, 'r', k_b, s_b, 'b', 'LineWidth', 0.9);
title('Number of steps');
xlabel('Case')
ylabel('Step')
legend('Default Heuristic', 'Proposed Heuristic');
axis([0 length(testCase)-1 0 max(max(s_a), max(s_b))+20]);

subplot(4,1,2);
plot(k_a, t_a, 'r', k_b, t_b, 'b', 'LineWidth', 0.9);
title('Elapsed time');
xlabel('Case')
ylabel('Time')
legend('Default Heuristic', 'Proposed Heuristic');
axis([0 length(testCase)-1 0 max(max(t_a), max(t_b))+100]);

subplot(4,1,3);
plot(k_a, e_a, 'r', k_b, e_b, 'b', 'LineWidth', 0.9);
title('Number of expanded nodes');
xlabel('Case')
ylabel('Node')
legend('Default Heuristic', 'Proposed Heuristic');
axis([0 length(testCase)-1 0 max(max(e_a), max(e_b))+100]);

subplot(4,1,4);
stem(k_a, e_a - e_b, 'b', 'LineWidth', 0.9);
title('Differential of expanded nodes');
xlabel('Case')
ylabel('Node')
axis([0 length(testCase)-1 min(e_a - e_b)-10 max(e_a - e_b)+10]);
