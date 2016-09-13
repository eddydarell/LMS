//Calendar
var Calendar = function (o) {
    //Store div id
    this.divId = o.ParentID;

    // Days of week, starting on Sunday
    this.DaysOfWeek = o.DaysOfWeek;

    console.log("this.DaysOfWeek == ", this.DaysOfWeek)

    // Months, stating on January
    this.Months = o.Months;

    console.log("this.Months == ", this.Months)

    // Set the current month, year
    var d = new Date();

    console.log("d == ", d)

    this.CurrentMonth = d.getMonth();

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    this.CurrentYear = d.getFullYear();

    console.log("this.CurrentYear == ", this.CurrentYear);

    var f = o.Format;

    console.log("o == ", o);

    console.log("f == ", f);

    //this.f = typeof(f) == 'string' ? f.charAt(0).toUpperCase() : 'M';

    if (typeof (f) == 'string') {
        this.f = f.charAt(0).toUpperCase();
    } else {
        this.f = 'M';
    }

    console.log("this.f == ", this.f);
};

// Goes to next month
Calendar.prototype.nextMonth = function () {
    console.log("Calendar.prototype.nextMonth = function() {");

    if (this.CurrentMonth == 11) {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = 0;

        console.log("this.CurrentMonth == ", this.CurrentMonth);

        console.log("this.CurrentYear == ", this.CurrentYear);

        this.CurrentYear = this.CurrentYear + 1;

        console.log("this.CurrentYear == ", this.CurrentYear);
    } else {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = this.CurrentMonth + 1;

        console.log("this.CurrentMonth + 1 == ", this.CurrentMonth);
    }

    this.showCurrent();
};

// Goes to previous month
Calendar.prototype.previousMonth = function () {
    console.log("Calendar.prototype.previousMonth = function() {");

    if (this.CurrentMonth == 0) {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = 11;

        console.log("this.CurrentMonth == ", this.CurrentMonth);

        console.log("this.CurrentYear == ", this.CurrentYear);

        this.CurrentYear = this.CurrentYear - 1;

        console.log("this.CurrentYear == ", this.CurrentYear);
    } else {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = this.CurrentMonth - 1;

        console.log("this.CurrentMonth - 1 == ", this.CurrentMonth);
    }

    this.showCurrent();
};

// 
Calendar.prototype.previousYear = function () {
    console.log(" ");

    console.log("Calendar.prototype.previousYear = function() {");

    console.log("this.CurrentYear == " + this.CurrentYear);

    this.CurrentYear = this.CurrentYear - 1;

    console.log("this.CurrentYear - 1 i.e. this.CurrentYear == " + this.CurrentYear);

    this.showCurrent();
}

// 
Calendar.prototype.nextYear = function () {
    console.log(" ");

    console.log("Calendar.prototype.nextYear = function() {");

    console.log("this.CurrentYear == " + this.CurrentYear);

    this.CurrentYear = this.CurrentYear + 1;

    console.log("this.CurrentYear - 1 i.e. this.CurrentYear == " + this.CurrentYear);

    this.showCurrent();
}

// Show current month
Calendar.prototype.showCurrent = function () {
    console.log(" ");

    console.log("Calendar.prototype.showCurrent = function() {");

    console.log("this.CurrentYear == ", this.CurrentYear);

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    this.Calendar(this.CurrentYear, this.CurrentMonth);
};

// Show month (year, month)
Calendar.prototype.Calendar = function (y, m) {

    calendarEvents = {
        startTime: '10:00',
        endTime: '12:00',
        periodicity: 1,
        startDate: new Date(),
        endDate: new Date('10/10/2016'),
        title: 'Lecture',
        description: 'Test Description',
        priority: 1,
        nature: 1,
        course: 'Math'
    };

    calendarEvents2 = {
        startTime: '13:00',
        endTime: '15:00',
        periodicity: 2,
        startDate: new Date(),
        endDate: new Date('10/10/2017'),
        title: 'Exam',
        description: 'Test Description',
        priority: 3,
        nature: 3,
        course: 'Biology'
    };

    calendarEvents3 = {
        startTime: '08:00',
        endTime: '10:00',
        periodicity: 1,
        startDate: new Date(),
        endDate: new Date('10/10/2017'),
        title: 'Quiz',
        description: 'Test Description',
        priority: 2,
        nature: 2,
        course: 'Swedish'
    };

    var eventBox = '<div class="eventBox">' +
                        '<div class="eventNature' + calendarEvents.nature + '">' +
                            '<span class="title text-left">' + calendarEvents.title + '</span><span class="courseName">' + calendarEvents.course + '</span><br/>' +
                            '<span class="time text-right"><span class="glyphicon glyphicon-time"></span>  ' + calendarEvents.startTime + ' - ' + calendarEvents.endTime + '</span>' +
                        '</div>' +
                        '<div class="eventPriority' + calendarEvents.priority + '"></div>' +
                   '</div>';

    var eventBox2 = '<div class="eventBox">' +
                        '<div class="eventNature' + calendarEvents2.nature + '">' +
                            '<span class="title text-left">' + calendarEvents2.title + '</span><span class="courseName">' + calendarEvents2.course + '</span><br/>' +
                            '<span class="time text-right"><span class="glyphicon glyphicon-time"></span>  ' + calendarEvents2.startTime + ' - ' + calendarEvents2.endTime + '</span>' +
                        '</div>' +
                        '<div class="eventPriority' + calendarEvents2.priority + '"></div>' +
                   '</div>';

    var eventBox3 = '<div class="eventBox">' +
                       '<div class="eventNature' + calendarEvents3.nature + '">' +
                           '<span class="title text-left">' + calendarEvents3.title + '</span><span class="courseName">' + calendarEvents3.course + '</span><br/>' +
                           '<span class="time text-right"><span class="glyphicon glyphicon-time"></span>  ' + calendarEvents3.startTime + ' - ' + calendarEvents3.endTime + '</span>' +
                       '</div>' +
                       '<div class="eventPriority' + calendarEvents3.priority + '"></div>' +
                  '</div>';



    console.log(" ");

    console.log("Calendar.prototype.Calendar = function(y,m){");

    typeof (y) == 'number' ? this.CurrentYear = y : null;

    console.log("this.CurrentYear == ", this.CurrentYear);

    typeof (y) == 'number' ? this.CurrentMonth = m : null;

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    // 1st day of the selected month
    var firstDayOfCurrentMonth = new Date(y, m, 1).getDay();

    console.log("firstDayOfCurrentMonth == ", firstDayOfCurrentMonth);

    // Last date of the selected month
    var lastDateOfCurrentMonth = new Date(y, m + 1, 0).getDate();

    console.log("lastDateOfCurrentMonth == ", lastDateOfCurrentMonth);

    // Last day of the previous month
    console.log("m == ", m);

    var lastDateOfLastMonth = m == 0 ? new Date(y - 1, 11, 0).getDate() : new Date(y, m, 0).getDate();

    console.log("lastDateOfLastMonth == ", lastDateOfLastMonth);

    console.log("Print selected month and year.");

    // Write selected month and year. This HTML goes into <div id="year"></div>
    //var yearhtml = '<span class="yearspan">' + y + '</span>';

    // Write selected month and year. This HTML goes into <div id="month"></div>
    //var monthhtml = '<span class="monthspan">' + this.Months[m] + '</span>';

    // Write selected month and year. This HTML goes into <div id="month"></div>
    var monthandyearhtml = '<span id="monthandyearspan">' + this.Months[m] + ' - ' + y + '</span>';

    console.log("monthandyearhtml == " + monthandyearhtml);

    var html = '<table>';

    // Write the header of the days of the week
    html += '<tr>';

    console.log(" ");

    console.log("Write the header of the days of the week");

    for (var i = 0; i < 7; i++) {
        console.log("i == ", i);

        console.log("this.DaysOfWeek[i] == ", this.DaysOfWeek[i]);

        html += '<th class="daysheader">' + this.DaysOfWeek[i] + '</th>';
    }

    html += '</tr>';

    console.log("Before conditional operator this.f == ", this.f);

    //this.f = 'X';

    var p = dm = this.f == 'M' ? 1 : firstDayOfCurrentMonth == 0 ? -5 : 2;

    /*var p, dm;
  
    if(this.f =='M') {
      dm = 1;
  
      p = dm;
    } else {
      if(firstDayOfCurrentMonth == 0) {
        firstDayOfCurrentMonth == -5;
      } else {
        firstDayOfCurrentMonth == 2;
      }
    }*/

    console.log("After conditional operator");

    console.log("this.f == ", this.f);

    console.log("p == ", p);

    console.log("dm == ", dm);

    console.log("firstDayOfCurrentMonth == ", firstDayOfCurrentMonth);

    var cellvalue;

    for (var d, i = 0, z0 = 0; z0 < 6; z0++) {
        html += '<tr>';

        console.log("Inside 1st for loop - d == " + d + " | i == " + i + " | z0 == " + z0);

        for (var z0a = 0; z0a < 7; z0a++) {
            console.log("Inside 2nd for loop");

            console.log("z0a == " + z0a);

            d = i + dm - firstDayOfCurrentMonth;

            console.log("d outside if statm == " + d);

            // Dates from prev month
            if (d < 1) {
                console.log("d < 1");

                console.log("p before p++ == " + p);

                cellvalue = lastDateOfLastMonth - firstDayOfCurrentMonth + p++;

                console.log("p after p++ == " + p);

                console.log("cellvalue == " + cellvalue);

                html += '<td class="prevmonthdates"><div class="calendarDayBox">' +
                      '<span class="cellvaluespan">' + (cellvalue) + '</span></div><div class="calendarDayEventBox">' +
                      '</div>' +
                    '</td>';

                // Dates from next month
            }
            else if (d > lastDateOfCurrentMonth) {
                console.log("d > lastDateOfCurrentMonth");

                console.log("p before p++ == " + p);

                html += '<td class="nextmonthdates"><div class="calendarDayBox">' + (p++) + '</div><div class="calendarDayEventBox"></div>' + '</td>';

                console.log("p after p++ == " + p);

                // Current month dates
            }
            else {
                var today = new Date();
                var todayDate = today.getDate();
                var todayMonth = today.getMonth();
                var todayYear = today.getFullYear();

                var testDate = new Date(2016, 09, 22, 10, 30, 45, 30);
                var month = testDate.getMonth().toString();
                
                if (d == todayDate && todayMonth == this.CurrentMonth && todayYear == this.CurrentYear) {

                    html += '<td id="today"><div class="calendarDayBox">' + (d) + '</div><div class="calendarDayEventBox"><div id="date-' + d + '/' + this.CurrentMonth + '/' + this.CurrentYear + '" class="sch_date">' + eventBox3 + eventBox + eventBox2 + '</div></div>' + '</td>';

                    console.log("d inside else { == " + d);

                    p = 1;

                    console.log("p inside } else { == " + p);

                }
                else {
                    html += '<td class="currentmonthdates"><div class="calendarDayBox">' + (d) + '</div><div class="calendarDayEventBox"><div id="date-' + d + '/' + this.CurrentMonth + '/' + this.CurrentYear + '" class="sch_date"></div></div>' + '</td>';

                    console.log("d inside else { == " + d);

                    p = 1;

                    console.log("p inside } else { == " + p);
                }
            }

            if (i % 7 == 6 && d >= lastDateOfCurrentMonth) {
                console.log("INSIDE if (i % 7 == 6 && d >= lastDateOfCurrentMonth) {");

                console.log("i == " + i);

                console.log("d == " + d);

                console.log("z0 == " + z0);

                z0 = 10; // no more rows
            }

            console.log("i before i++ == " + i);

            i++;

            console.log("i after i++ == " + i);
        }

        html += '</tr>';
    }

    // Closes table
    html += '</table>';

    // Write HTML to the div
    //document.getElementById("year").innerHTML = yearhtml;

    //document.getElementById("month").innerHTML = monthhtml;

    document.getElementById("monthandyear").innerHTML = monthandyearhtml;

    document.getElementById(this.divId).innerHTML = html;
};


//JQuery
(function () {


})();

//Angular
(function () {
    var app = angular.module("Main", []);

    var enrollStudentController = app.controller("enrollStudentCtrl", function ($scope, $http, $window, $timeout) {
        $scope.studentFound = false;
        $scope.studentNotFound = false;
        $scope.enrollSuccessful = false;
        $scope.searchDetails = {
            resultFormat: "json",
            email: ""
        };
        $scope.student = {
            name: '',
            email: '',
            id: ''
        };


        var searchStudentByEmail = function () {
            //Load student

            $http({
                method: 'GET',
                url: '/Search/SearchUserByEmail',
                params: $scope.searchDetails
            }).then(function successCallback(response) {
                if (response.data.error !== undefined) {
                    $scope.studentNotFound = true;
                    return;
                }
                $scope.student.name = response.data.Name;
                $scope.student.email = response.data.Email;
                $scope.student.id = response.data.ID;
                $scope.studentFound = true;

                console.log(response.data);
            }, function errorCallback(response) {
                $scope.studentNotFound = true;
                console.log(response);
            });

        };

        var enrollStudent = function (courseID) {
            $http({
                method: 'GET',
                url: '/Course/EnrollStudent',
                params: {
                    studentID: $scope.student.id,
                    courseID: courseID,
                    resultFormat: 'json'
                }
            }).then(function successCallback(response) {
                $scope.studentFound = false;
                $scope.enrollSuccessful = true;

                //To-Do: Fix this timeout callback
                $timeout($window.location.reload, 3000);

                console.log(response.data);
            }, function errorCallback(response) {
                console.log(response);
            });
        };

        $scope.enrollStudent = enrollStudent;
        $scope.searchStudentByEmail = searchStudentByEmail;
    });

})();