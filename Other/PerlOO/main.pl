package MatrixResult;

sub new{
    $class = shift;
    ($success) = @_;
    $self = bless{
        success => $success
    }, $class;
    return $self;
}

sub success{
    $self = shift;

    if (@_){
        $self->{success} = shift;
    }
    return $self->{success};
}

package Main;

$result = MatrixResult->new("sdf");
print "what: $result->{success}";
$result->success(1);
print "what: $result->{success}";
